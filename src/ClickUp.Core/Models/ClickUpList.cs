using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class ClickUpList
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("orderindex")]
    public int OrderIndex { get; set; }

    [JsonPropertyName("status")]
    public ClickUpListStatus? Status { get; set; }

    [JsonPropertyName("priority")]
    public ClickUpPriority? Priority { get; set; }

    [JsonPropertyName("assignee")]
    public ClickUpUser? Assignee { get; set; }

    [JsonPropertyName("task_count")]
    public int TaskCount { get; set; }

    [JsonPropertyName("due_date")]
    public string? DueDate { get; set; }

    [JsonPropertyName("due_date_time")]
    public bool DueDateTime { get; set; }

    [JsonPropertyName("start_date")]
    public string? StartDate { get; set; }

    [JsonPropertyName("start_date_time")]
    public bool StartDateTime { get; set; }

    [JsonPropertyName("folder")]
    public ClickUpFolderReference? Folder { get; set; }

    [JsonPropertyName("space")]
    public ClickUpSpaceReference? Space { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("override_statuses")]
    public bool OverrideStatuses { get; set; }

    [JsonPropertyName("statuses")]
    public List<ClickUpStatus>? Statuses { get; set; }

    [JsonPropertyName("permission_level")]
    public string? PermissionLevel { get; set; }
}

public class ClickUpListStatus
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("hide_label")]
    public bool HideLabel { get; set; }
}

public class ClickUpFolderReference
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("access")]
    public bool Access { get; set; }
}

public class CreateListRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("due_date")]
    public long? DueDate { get; set; }

    [JsonPropertyName("due_date_time")]
    public bool? DueDateTime { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("assignee")]
    public long? Assignee { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public class ClickUpListsResponse
{
    [JsonPropertyName("lists")]
    public List<ClickUpList> Lists { get; set; } = new();
}