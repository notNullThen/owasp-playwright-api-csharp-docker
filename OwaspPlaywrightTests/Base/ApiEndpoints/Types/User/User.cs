using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.ApiEndpoints.Types.User;

public class User
{
    [JsonPropertyName("response")]
    public required UserResponse Response { get; set; }

    [JsonPropertyName("payload")]
    public required UserPayload Payload { get; set; }
};
