using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.ApiEndpoints.Types.SecurityAnswers;

public class SecurityAnswersPayload
{
    [JsonPropertyName("UserId")]
    public required int UserId { get; set; }

    [JsonPropertyName("answer")]
    public required string Answer { get; set; }

    [JsonPropertyName("SecurityQuestionId")]
    public required int SecurityQuestionId { get; set; }
};
