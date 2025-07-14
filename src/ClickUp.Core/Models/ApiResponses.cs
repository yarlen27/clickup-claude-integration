using System.Text.Json.Serialization;

namespace ClickUp.Core.Models;

public class ClickUpTasksResponse
{
    [JsonPropertyName("tasks")]
    public List<ClickUpTask> Tasks { get; set; } = new();
}

public class ClickUpTeamsResponse
{
    [JsonPropertyName("teams")]
    public List<ClickUpTeam> Teams { get; set; } = new();
}

public class ClickUpTeam
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("members")]
    public List<ClickUpMember>? Members { get; set; }
}

public class ClickUpMember
{
    [JsonPropertyName("user")]
    public ClickUpUser User { get; set; } = new();
}

public class ClickUpApiError
{
    [JsonPropertyName("err")]
    public string Error { get; set; } = string.Empty;

    [JsonPropertyName("ECODE")]
    public string ErrorCode { get; set; } = string.Empty;
}