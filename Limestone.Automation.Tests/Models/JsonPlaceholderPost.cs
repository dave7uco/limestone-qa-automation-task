using System.Text.Json.Serialization;

namespace Limestone.Automation.Tests.Models;

public class JsonPlaceholderPost
{
    [JsonPropertyName("userId")]
    public int UserId { get; init; }

    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Body { get; init; }
}
