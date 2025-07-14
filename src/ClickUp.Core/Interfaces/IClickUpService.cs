using ClickUp.Core.Models;

namespace ClickUp.Core.Interfaces;

public interface IClickUpService
{
    // Teams/Workspaces
    Task<List<ClickUpTeam>> GetTeamsAsync();
    
    // Members
    Task<List<MemberSearchResult>> GetAllMembersAsync();
    Task<List<MemberSearchResult>> SearchMembersAsync(string query);
    Task<MemberSearchResult?> FindMemberAsync(string identifier);
    Task<List<ClickUpListMember>> GetListMembersAsync(string listId);
    Task<List<long>> ResolveMemberIdsAsync(List<string> identifiers);
    
    // Spaces
    Task<List<ClickUpSpace>> GetSpacesAsync(string teamId);
    Task<ClickUpSpace?> CreateSpaceAsync(string teamId, CreateSpaceRequest request);
    
    // Folders
    Task<List<ClickUpFolder>> GetFoldersAsync(string spaceId);
    Task<ClickUpFolder?> CreateFolderAsync(string spaceId, CreateFolderRequest request);
    
    // Lists
    Task<List<ClickUpList>> GetListsAsync(string folderId);
    Task<List<ClickUpList>> GetFolderlessListsAsync(string spaceId);
    Task<ClickUpList?> CreateListAsync(string folderId, CreateListRequest request);
    Task<ClickUpList?> CreateFolderlessListAsync(string spaceId, CreateListRequest request);
    
    // Tasks
    Task<List<ClickUpTask>> GetTasksAsync(string listId);
    Task<ClickUpTask?> GetTaskAsync(string taskId);
    Task<ClickUpTask?> CreateTaskAsync(string listId, CreateTaskRequest request);
    Task<ClickUpTask?> UpdateTaskAsync(string taskId, CreateTaskRequest request);
    Task<bool> DeleteTaskAsync(string taskId);
}