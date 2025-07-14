using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class CreateTaskRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("assignees")]
    public List<long>? Assignees { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("due_date")]
    public long? DueDate { get; set; }

    [JsonPropertyName("due_date_time")]
    public bool? DueDateTime { get; set; }

    [JsonPropertyName("start_date")]
    public long? StartDate { get; set; }

    [JsonPropertyName("start_date_time")]
    public bool? StartDateTime { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

    [JsonPropertyName("time_estimate")]
    public long? TimeEstimate { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }
}