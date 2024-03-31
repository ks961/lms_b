using System.Security.Claims;
using lms_b.Data;
using lms_b.Services;
using lms_b.Utils;

namespace lms_b.Middlewares;

public class AuthMiddleware
{

    public static readonly string CookieName = "__logtoken";

    public static void UnAuthorizeRequest(HttpContext context, string responseMessage)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        _ = context.Response.WriteAsync(responseMessage ?? "");
    }

    public static async ValueTask<object?> Authenticate(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next
    )
    {
        string cookie = context.HttpContext.Request.Cookies[CookieName] ?? "";

        try {
            Result<ClaimsPrincipal, string> result = JWTService.ValidateJwtToken(cookie, "/login", "lms_f");
            
            if (result.IsErr)
                UnAuthorizeRequest(context.HttpContext, result.Error ?? "");

        } catch {
            UnAuthorizeRequest(context.HttpContext, "Bad Authentication Request" ?? "");
        }

        return await next(context);
    }

    // private static bool IsAuthenticated(string cookie)
    // {
    //     return ActiveSessionTracker.IsThereAClient(cookie);
    // }
}
