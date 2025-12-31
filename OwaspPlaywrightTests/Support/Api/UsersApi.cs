using System.Text.Json.Serialization;
using OwaspPlaywrightTests.Support.Api.Base;

namespace OwaspPlaywrightTests.Support.Api;

public class UsersApi() : ApiBase("api/Users")
{
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

    public class Data
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

    public class UserResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }

        [JsonPropertyName("data")]
        public required Data Data { get; set; }
    };

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

    public class User
    {
        [JsonPropertyName("response")]
        public required UserResponse Response { get; set; }

        [JsonPropertyName("payload")]
        public required UserPayload Payload { get; set; }
    };

    public ApiAction<UserResponse> PostUser(UserPayload? payload = null) =>
        Action<UserResponse>(new() { Method = Types.ApiHttpMethod.POST, Body = payload });
}
