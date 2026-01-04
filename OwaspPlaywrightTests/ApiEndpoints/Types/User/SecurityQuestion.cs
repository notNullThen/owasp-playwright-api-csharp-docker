using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.User;

public class SecurityQuestion
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("question")]
    public required string Question { get; set; }

    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public required string UpdatedAt { get; set; }
};
