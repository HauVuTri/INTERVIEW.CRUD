namespace INTERVIEW.CRUD.Models;

public class JwtSetting
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public TimeSpan TokenExpiresAfter { get; set; }
    public TimeSpan ExpiresRefreshToken { get; set; }
}