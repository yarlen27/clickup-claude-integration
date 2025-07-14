using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class ClickUpFolder
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("orderindex")]
    public int OrderIndex { get; set; }

    [JsonPropertyName("override_statuses")]
    public bool OverrideStatuses { get; set; }

    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }

    [JsonPropertyName("space")]
    public ClickUpSpaceReference? Space { get; set; }

    [JsonPropertyName("task_count")]
    public string? TaskCount { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("statuses")]
    public List<ClickUpStatus>? Statuses { get; set; }

    [JsonPropertyName("lists")]
    public List<ClickUpList>? Lists { get; set; }
}

public class ClickUpSpaceReference
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class CreateFolderRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class ClickUpFoldersResponse
{
    [JsonPropertyName("folders")]
    public List<ClickUpFolder> Folders { get; set; } = new();
}