using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.SecurityAnswers;

public class SecurityQuestionResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("data")]
    public required Data Data { get; set; }
};
