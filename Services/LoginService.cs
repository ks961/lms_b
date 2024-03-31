using lms_b.Middlewares;
using lms_b.Utils;
using Microsoft.EntityFrameworkCore;

namespace lms_b.Services;

public class LoginService
{
    private static readonly AppDbContext DBContext = AppDbContext.GetDBContext() 
        ?? throw new Exception("Database Error: No connection established"); 

    private static readonly int SessionTimout = 30;
    public static async Task<bool> IsUserCredentialValid(LoginRequestDto userRequest)
    {
        return await DBContext.IsUserCredentialValid(userRequest);
    }

    public static string GenerateSessionCookie(LoginRequestDto userRequest)
    {
        var user = new Dictionary<string, string>
        {
            { "email", userRequest.Email },
            { "role", "student" }
        };

        string jwtToken = JWTService.GenerateJwtToken(
            "/login",
            "lms_f",
            user,
            SessionTimout
        );

        var cookieValue = jwtToken;
        var expiration = DateTime.UtcNow.AddMinutes(SessionTimout);

        var cookieString = 
            $"{AuthMiddleware.CookieName}={cookieValue}; Expires={expiration.ToUniversalTime().ToString("R")}; HttpOnly";
        
        return cookieString;
    }
}
