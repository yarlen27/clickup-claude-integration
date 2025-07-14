using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class ClickUpSpace
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("private")]
    public bool Private { get; set; }

    [JsonPropertyName("statuses")]
    public List<ClickUpStatus>? Statuses { get; set; }

    [JsonPropertyName("multiple_assignees")]
    public bool MultipleAssignees { get; set; }

    [JsonPropertyName("features")]
    public ClickUpSpaceFeatures? Features { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }
}

public class ClickUpSpaceFeatures
{
    [JsonPropertyName("due_dates")]
    public ClickUpFeatureConfig? DueDates { get; set; }

    [JsonPropertyName("time_tracking")]
    public ClickUpFeatureConfig? TimeTracking { get; set; }

    [JsonPropertyName("tags")]
    public ClickUpFeatureConfig? Tags { get; set; }

    [JsonPropertyName("time_estimates")]
    public ClickUpFeatureConfig? TimeEstimates { get; set; }

    [JsonPropertyName("checklists")]
    public ClickUpFeatureConfig? Checklists { get; set; }

    [JsonPropertyName("custom_fields")]
    public ClickUpFeatureConfig? CustomFields { get; set; }

    [JsonPropertyName("remap_dependencies")]
    public ClickUpFeatureConfig? RemapDependencies { get; set; }

    [JsonPropertyName("dependency_warning")]
    public ClickUpFeatureConfig? DependencyWarning { get; set; }

    [JsonPropertyName("portfolios")]
    public ClickUpFeatureConfig? Portfolios { get; set; }
}

public class ClickUpFeatureConfig
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}

public class CreateSpaceRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("multiple_assignees")]
    public bool? MultipleAssignees { get; set; }

    [JsonPropertyName("features")]
    public ClickUpSpaceFeatures? Features { get; set; }
}

public class ClickUpSpacesResponse
{
    [JsonPropertyName("spaces")]
    public List<ClickUpSpace> Spaces { get; set; } = new();
}