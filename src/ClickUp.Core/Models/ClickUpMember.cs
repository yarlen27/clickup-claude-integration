using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class ClickUpMemberDetails
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("profilePicture")]
    public string? ProfilePicture { get; set; }

    [JsonPropertyName("initials")]
    public string Initials { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public int Role { get; set; }

    [JsonPropertyName("role_key")]
    public string RoleKey { get; set; } = string.Empty;

    [JsonPropertyName("custom_role")]
    public string? CustomRole { get; set; }

    [JsonPropertyName("last_active")]
    public string? LastActive { get; set; }

    [JsonPropertyName("date_joined")]
    public string? DateJoined { get; set; }

    [JsonPropertyName("date_invited")]
    public string? DateInvited { get; set; }
}

public class ClickUpListMembersResponse
{
    [JsonPropertyName("members")]
    public List<ClickUpListMember> Members { get; set; } = new();
}

public class ClickUpListMember
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("profilePicture")]
    public string? ProfilePicture { get; set; }

    [JsonPropertyName("initials")]
    public string Initials { get; set; } = string.Empty;
}

public class MemberSearchResult
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleKey { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class MemberAssignmentRequest
{
    public string TaskId { get; set; } = string.Empty;
    public List<string> MemberIdentifiers { get; set; } = new(); // Can be usernames, emails, or IDs
    public bool ReplaceExisting { get; set; } = false;
}