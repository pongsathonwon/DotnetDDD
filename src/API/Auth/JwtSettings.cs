namespace API.Auth;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";

    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public int ExpiryInMinutes { get; init; } = 60;
}
