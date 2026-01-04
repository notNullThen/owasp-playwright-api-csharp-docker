using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.User;

public class UserData
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("role")]
    public required string Role { get; set; }

    [JsonPropertyName("deluxeToken")]
    public required string DeluxeToken { get; set; }

    [JsonPropertyName("lastLoginIp")]
    public required string LastLoginIp { get; set; }

    [JsonPropertyName("profileImage")]
    public required string ProfileImage { get; set; }

    [JsonPropertyName("isActive")]
    public required bool IsActive { get; set; }

    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("updatedAt")]
    public required string UpdatedAt { get; set; }

    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }

    [JsonPropertyName("deletedAt")]
    public required string DeletedAt { get; set; }
}
