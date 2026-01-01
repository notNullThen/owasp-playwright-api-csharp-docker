using System.Text.Json.Serialization;
using OwaspPlaywrightTests.Support.Api.Base;

namespace OwaspPlaywrightTests.Support.Api;

public class SecurityAnswersApi() : ApiBase("api/SecurityAnswers")
{
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

    public class SecurityQuestionResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }

        [JsonPropertyName("data")]
        public required Data Data { get; set; }
    };

    public class SecurityAnswersPayload
    {
        [JsonPropertyName("UserId")]
        public required int UserId { get; set; }

        [JsonPropertyName("answer")]
        public required string Answer { get; set; }

        [JsonPropertyName("SecurityQuestionId")]
        public required int SecurityQuestionId { get; set; }
    };

    public ApiAction<SecurityQuestionResponse> PostSecurityAnswers(
        SecurityAnswersPayload payload
    ) =>
        Action<SecurityQuestionResponse>(
            new() { Method = Types.ApiHttpMethod.POST, Body = payload }
        );
}
