using lms_b.Data;

namespace lms_b.Middlewares;

public class AuthMiddleware
{

    public static async ValueTask<object?> Authenticate(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next
    )
    {
        string cookie = context.HttpContext.Request.Cookies["cookie"] ?? "";

        if (!IsAuthenticated(cookie))
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return await next(context);
        }

        return await next(context);
    }

    private static bool IsAuthenticated(string cookie)
    {
            return LoggedInUsers.IsThereAClient(cookie);
    }
}
