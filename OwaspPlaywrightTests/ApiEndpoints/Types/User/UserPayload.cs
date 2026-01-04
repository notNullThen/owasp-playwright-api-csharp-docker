using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.User;

public class UserPayload
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }

    [JsonPropertyName("passwordRepeat")]
    public required string PasswordRepeat { get; set; }

    [JsonPropertyName("securityQuestion")]
    public required SecurityQuestion SecurityQuestion { get; set; }

    [JsonPropertyName("securityAnswer")]
    public required string SecurityAnswer { get; set; }
};
