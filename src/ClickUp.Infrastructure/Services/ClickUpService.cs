using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ClickUp.Core.Interfaces;
using ClickUp.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ClickUp.Infrastructure.Services;

public class ClickUpService : IClickUpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ClickUpService> _logger;
    private readonly string _apiToken;
    private const string BaseUrl = "https://api.clickup.com";

    public ClickUpService(HttpClient httpClient, IConfiguration configuration, ILogger<ClickUpService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiToken = configuration["ClickUp:ApiToken"] ?? throw new ArgumentException("ClickUp API token not configured");
        
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _httpClient.DefaultRequestHeaders.Add("Authorization", _apiToken);
    }

    public async Task<List<ClickUpTeam>> GetTeamsAsync()
    {
        try
        {
            _logger.LogInformation("Getting teams from ClickUp API");
            
            var response = await _httpClient.GetAsync("/api/v2/team");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get teams. Status: {StatusCode}, Content: {Content}", 
                    response.StatusCode, content);
                return new List<ClickUpTeam>();
            }

            var teamsResponse = JsonSerializer.Deserialize<ClickUpTeamsResponse>(content);
            return teamsResponse?.Teams ?? new List<ClickUpTeam>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting teams from ClickUp API");
            return new List<ClickUpTeam>();
        }
    }

    public async Task<List<MemberSearchResult>> GetAllMembersAsync()
    {
        try
        {
            _logger.LogInformation("Getting all members from all teams");
            
            var teams = await GetTeamsAsync();
            var allMembers = new List<MemberSearchResult>();

            foreach (var team in teams)
            {
                if (team.Members != null)
                {
                    foreach (var member in team.Members)
                    {
                        allMembers.Add(new MemberSearchResult
                        {
                            Id = member.User.Id,
                            Username = member.User.Username,
                            Email = member.User.Email,
                            RoleKey = member.User.RoleKey ?? "member",
                            TeamName = team.Name,
                            TeamId = team.Id,
                            IsActive = !string.IsNullOrEmpty(member.User.LastActive)
                        });
                    }
                }
            }

            return allMembers;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all members");
            return new List<MemberSearchResult>();
        }
    }

    public async Task<List<MemberSearchResult>> SearchMembersAsync(string query)
    {
        try
        {
            _logger.LogInformation("Searching members with query: {Query}", query);
            
            var allMembers = await GetAllMembersAsync();
            var lowerQuery = query.ToLowerInvariant();

            return allMembers.Where(m => 
                m.Username.ToLowerInvariant().Contains(lowerQuery) ||
                m.Email.ToLowerInvariant().Contains(lowerQuery) ||
                m.Id.ToString().Contains(query)
            ).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching members with query: {Query}", query);
            return new List<MemberSearchResult>();
        }
    }

    public async Task<MemberSearchResult?> FindMemberAsync(string identifier)
    {
        try
        {
            _logger.LogInformation("Finding member with identifier: {Identifier}", identifier);
            
            var allMembers = await GetAllMembersAsync();
            var lowerIdentifier = identifier.ToLowerInvariant();

            // Try exact matches first
            var exactMatch = allMembers.FirstOrDefault(m => 
                m.Username.ToLowerInvariant() == lowerIdentifier ||
                m.Email.ToLowerInvariant() == lowerIdentifier ||
                m.Id.ToString() == identifier
            );

            if (exactMatch != null)
                return exactMatch;

            // Try partial matches
            return allMembers.FirstOrDefault(m => 
                m.Username.ToLowerInvariant().Contains(lowerIdentifier) ||
                m.Email.ToLowerInvariant().Contains(lowerIdentifier)
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error finding member with identifier: {Identifier}", identifier);
            return null;
        }
    }

    public async Task<List<ClickUpListMember>> GetListMembersAsync(string listId)
    {
        try
        {
            _logger.LogInformation("Getting members for list {ListId}", listId);
            
            var response = await _httpClient.GetAsync($"/api/v2/list/{listId}/member");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get list members for list {ListId}. Status: {StatusCode}, Content: {Content}", 
                    listId, response.StatusCode, content);
                return new List<ClickUpListMember>();
            }

            var membersResponse = JsonSerializer.Deserialize<ClickUpListMembersResponse>(content);
            return membersResponse?.Members ?? new List<ClickUpListMember>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting list members for list {ListId}", listId);
            return new List<ClickUpListMember>();
        }
    }

    public async Task<List<long>> ResolveMemberIdsAsync(List<string> identifiers)
    {
        try
        {
            _logger.LogInformation("Resolving member IDs for identifiers: {Identifiers}", string.Join(", ", identifiers));
            
            var resolvedIds = new List<long>();
            
            foreach (var identifier in identifiers)
            {
                // Try to parse as ID first
                if (long.TryParse(identifier, out var directId))
                {
                    resolvedIds.Add(directId);
                    continue;
                }

                // Search for member
                var member = await FindMemberAsync(identifier);
                if (member != null)
                {
                    resolvedIds.Add(member.Id);
                }
                else
                {
                    _logger.LogWarning("Could not resolve member identifier: {Identifier}", identifier);
                }
            }

            return resolvedIds;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resolving member IDs");
            return new List<long>();
        }
    }

    public async Task<List<ClickUpSpace>> GetSpacesAsync(string teamId)
    {
        try
        {
            _logger.LogInformation("Getting spaces for team {TeamId}", teamId);
            
            var response = await _httpClient.GetAsync($"/api/v2/team/{teamId}/space");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get spaces for team {TeamId}. Status: {StatusCode}, Content: {Content}", 
                    teamId, response.StatusCode, content);
                return new List<ClickUpSpace>();
            }

            var spacesResponse = JsonSerializer.Deserialize<ClickUpSpacesResponse>(content);
            return spacesResponse?.Spaces ?? new List<ClickUpSpace>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting spaces for team {TeamId}", teamId);
            return new List<ClickUpSpace>();
        }
    }

    public async Task<ClickUpSpace?> CreateSpaceAsync(string teamId, CreateSpaceRequest request)
    {
        try
        {
            _logger.LogInformation("Creating space in team {TeamId}: {SpaceName}", teamId, request.Name);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"/api/v2/team/{teamId}/space", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to create space. Status: {StatusCode}, Content: {Content}", 
                    response.StatusCode, responseContent);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpSpace>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating space in team {TeamId}", teamId);
            return null;
        }
    }

    public async Task<List<ClickUpFolder>> GetFoldersAsync(string spaceId)
    {
        try
        {
            _logger.LogInformation("Getting folders for space {SpaceId}", spaceId);
            
            var response = await _httpClient.GetAsync($"/api/v2/space/{spaceId}/folder");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get folders for space {SpaceId}. Status: {StatusCode}, Content: {Content}", 
                    spaceId, response.StatusCode, content);
                return new List<ClickUpFolder>();
            }

            var foldersResponse = JsonSerializer.Deserialize<ClickUpFoldersResponse>(content);
            return foldersResponse?.Folders ?? new List<ClickUpFolder>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting folders for space {SpaceId}", spaceId);
            return new List<ClickUpFolder>();
        }
    }

    public async Task<ClickUpFolder?> CreateFolderAsync(string spaceId, CreateFolderRequest request)
    {
        try
        {
            _logger.LogInformation("Creating folder in space {SpaceId}: {FolderName}", spaceId, request.Name);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"/api/v2/space/{spaceId}/folder", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to create folder. Status: {StatusCode}, Content: {Content}", 
                    response.StatusCode, responseContent);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpFolder>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating folder in space {SpaceId}", spaceId);
            return null;
        }
    }

    public async Task<List<ClickUpList>> GetListsAsync(string folderId)
    {
        try
        {
            _logger.LogInformation("Getting lists for folder {FolderId}", folderId);
            
            var response = await _httpClient.GetAsync($"/api/v2/folder/{folderId}/list");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get lists for folder {FolderId}. Status: {StatusCode}, Content: {Content}", 
                    folderId, response.StatusCode, content);
                return new List<ClickUpList>();
            }

            var listsResponse = JsonSerializer.Deserialize<ClickUpListsResponse>(content);
            return listsResponse?.Lists ?? new List<ClickUpList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting lists for folder {FolderId}", folderId);
            return new List<ClickUpList>();
        }
    }

    public async Task<List<ClickUpList>> GetFolderlessListsAsync(string spaceId)
    {
        try
        {
            _logger.LogInformation("Getting folderless lists for space {SpaceId}", spaceId);
            
            var response = await _httpClient.GetAsync($"/api/v2/space/{spaceId}/list");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get folderless lists for space {SpaceId}. Status: {StatusCode}, Content: {Content}", 
                    spaceId, response.StatusCode, content);
                return new List<ClickUpList>();
            }

            var listsResponse = JsonSerializer.Deserialize<ClickUpListsResponse>(content);
            return listsResponse?.Lists ?? new List<ClickUpList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting folderless lists for space {SpaceId}", spaceId);
            return new List<ClickUpList>();
        }
    }

    public async Task<ClickUpList?> CreateListAsync(string folderId, CreateListRequest request)
    {
        try
        {
            _logger.LogInformation("Creating list in folder {FolderId}: {ListName}", folderId, request.Name);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"/api/v2/folder/{folderId}/list", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to create list in folder. Status: {StatusCode}, Content: {Content}", 
                    response.StatusCode, responseContent);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpList>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating list in folder {FolderId}", folderId);
            return null;
        }
    }

    public async Task<ClickUpList?> CreateFolderlessListAsync(string spaceId, CreateListRequest request)
    {
        try
        {
            _logger.LogInformation("Creating folderless list in space {SpaceId}: {ListName}", spaceId, request.Name);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"/api/v2/space/{spaceId}/list", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to create folderless list. Status: {StatusCode}, Content: {Content}", 
                    response.StatusCode, responseContent);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpList>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating folderless list in space {SpaceId}", spaceId);
            return null;
        }
    }

    public async Task<List<ClickUpTask>> GetTasksAsync(string listId)
    {
        try
        {
            _logger.LogInformation("Getting tasks for list {ListId}", listId);
            
            var response = await _httpClient.GetAsync($"/api/v2/list/{listId}/task");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get tasks for list {ListId}. Status: {StatusCode}, Content: {Content}", 
                    listId, response.StatusCode, content);
                return new List<ClickUpTask>();
            }

            var tasksResponse = JsonSerializer.Deserialize<ClickUpTasksResponse>(content);
            return tasksResponse?.Tasks ?? new List<ClickUpTask>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tasks for list {ListId}", listId);
            return new List<ClickUpTask>();
        }
    }

    public async Task<ClickUpTask?> GetTaskAsync(string taskId)
    {
        try
        {
            _logger.LogInformation("Getting task {TaskId}", taskId);
            
            var response = await _httpClient.GetAsync($"/api/v2/task/{taskId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get task {TaskId}. Status: {StatusCode}, Content: {Content}", 
                    taskId, response.StatusCode, content);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpTask>(content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting task {TaskId}", taskId);
            return null;
        }
    }

    public async Task<ClickUpTask?> CreateTaskAsync(string listId, CreateTaskRequest request)
    {
        try
        {
            _logger.LogInformation("Creating task in list {ListId}: {TaskName}", listId, request.Name);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"/api/v2/list/{listId}/task", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to create task. Status: {StatusCode}, Content: {Content}", 
                    response.StatusCode, responseContent);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpTask>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task in list {ListId}", listId);
            return null;
        }
    }

    public async Task<ClickUpTask?> UpdateTaskAsync(string taskId, CreateTaskRequest request)
    {
        try
        {
            _logger.LogInformation("Updating task {TaskId}", taskId);
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"/api/v2/task/{taskId}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to update task {TaskId}. Status: {StatusCode}, Content: {Content}", 
                    taskId, response.StatusCode, responseContent);
                return null;
            }

            return JsonSerializer.Deserialize<ClickUpTask>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task {TaskId}", taskId);
            return null;
        }
    }

    public async Task<bool> DeleteTaskAsync(string taskId)
    {
        try
        {
            _logger.LogInformation("Deleting task {TaskId}", taskId);
            
            var response = await _httpClient.DeleteAsync($"/api/v2/task/{taskId}");

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to delete task {TaskId}. Status: {StatusCode}, Content: {Content}", 
                    taskId, response.StatusCode, content);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task {TaskId}", taskId);
            return false;
        }
    }
}