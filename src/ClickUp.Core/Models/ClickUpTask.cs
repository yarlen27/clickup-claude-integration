using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class ClickUpTask
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("status")]
    public ClickUpStatus Status { get; set; } = new();

    [JsonPropertyName("priority")]
    public ClickUpPriority? Priority { get; set; }

    [JsonPropertyName("assignees")]
    public List<ClickUpUser> Assignees { get; set; } = new();

    [JsonPropertyName("date_created")]
    public string DateCreated { get; set; } = string.Empty;

    [JsonPropertyName("date_updated")]
    public string DateUpdated { get; set; } = string.Empty;

    [JsonPropertyName("due_date")]
    public string? DueDate { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}

public class ClickUpStatus
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
}

public class ClickUpPriority
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public string Priority { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
}

public class ClickUpUser
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
    public string? RoleKey { get; set; }

    [JsonPropertyName("custom_role")]
    public string? CustomRole { get; set; }

    [JsonPropertyName("last_active")]
    public string? LastActive { get; set; }

    [JsonPropertyName("date_joined")]
    public string? DateJoined { get; set; }

    [JsonPropertyName("date_invited")]
    public string? DateInvited { get; set; }
}