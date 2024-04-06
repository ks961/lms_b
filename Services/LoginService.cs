using lms_b.Middlewares;

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
        var currentDateTime = DateTime.UtcNow;

        string timestamp = currentDateTime.ToUniversalTime().ToString("R");

        var user = new Dictionary<string, string>
        {
            { "email", userRequest.Email },
            {"timestamp", timestamp},
            { "role", "student" } // Temporary
        };

        string cookieValue = JWTService.GenerateJwtToken(
            "/login",
            "lms_f",
            user,
            SessionTimout
        );

        var expirationDateTimeAsStr = currentDateTime.AddMinutes(SessionTimout)
            .ToUniversalTime().ToString("R");
        var cookieString = 
            $"{AuthMiddleware.CookieName}={cookieValue}; Expires={expirationDateTimeAsStr}; HttpOnly";
        
        return cookieString;
    }
}
