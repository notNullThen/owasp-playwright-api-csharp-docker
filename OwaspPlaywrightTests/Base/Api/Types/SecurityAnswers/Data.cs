using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.Api.Types.SecurityAnswers;

public class Data
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("UserId")]
    public required int UserId { get; set; }

    [JsonPropertyName("answer")]
    public required string Answer { get; set; }

    [JsonPropertyName("SecurityQuestionId")]
    public required int SecurityQuestionId { get; set; }

    [JsonPropertyName("updatedAt")]
    public required string UpdatedAt { get; set; }

    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }
};
