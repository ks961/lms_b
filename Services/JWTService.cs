using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using lms_b.Utils;
using Microsoft.IdentityModel.Tokens;

namespace lms_b.Services;

public class JWTService
{
    private static readonly string JWT_SEC = Environment.GetEnvironmentVariable("JWT_SEC")
        ?? throw new Exception("JWT: No Secret Key Found!");
    public static string GenerateJwtToken(string issuer, string audience, Dictionary<string, string> claims, int expirationMinutes = 30)
    {
        var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JWT_SEC));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.Select(c => new Claim(c.Key, c.Value))),
            Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    public static Result<ClaimsPrincipal, string> ValidateJwtToken(string token, string issuer, string audience)
{
    try
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.UTF8.GetBytes(JWT_SEC);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Adjust if needed for clock differences
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        return Result<ClaimsPrincipal, string>.Ok(principal);
    }
    catch (SecurityTokenValidationException ex)
    {
        // Handle invalid token gracefully (e.g., log error, return appropriate response)
        Console.WriteLine(ex.Message);
        return Result<ClaimsPrincipal, string>.Err("Invalid authentication token!");
    }
}
}
