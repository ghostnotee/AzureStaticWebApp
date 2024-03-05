// ReSharper disable UnusedParameter.Local
namespace Infrastructure.Settings;

public class AppSettings
{
    public AppSettings()
    {
    }

    public AppSettings(bool isDevelopment)
    {
        SqlServerConnectionStrings = Environment.GetEnvironmentVariable("SqlServerConnectionStrings");
        Secret = Environment.GetEnvironmentVariable("Secret");
        ExpireMinutes = int.Parse(Environment.GetEnvironmentVariable("ExpireMinutes")!);
        Issuer = Environment.GetEnvironmentVariable("Issuer");
        Audience = Environment.GetEnvironmentVariable("Audience");
    }

    public string? SqlServerConnectionStrings { get; set; }
    public string? Secret { get; init; }
    public int ExpireMinutes { get; init; }
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public string? GoogleClientId { get; set; }
    public string? GoogleProjectId { get; set; }
    public string? GoogleAuthUri { get; set; }
    public string? GoogleTokenUri { get; set; }
    public string? GoogleAuthProviderX509CertUrl { get; set; }
    public string? GoogleClientSecret { get; set; }
    public string? GoogleRedirectUri { get; set; }
}