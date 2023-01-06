namespace Application.Options;

public class JwtOptions
{
    public const string Key = nameof(JwtOptions);
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int ExpirationInHours { get; set; }
}
