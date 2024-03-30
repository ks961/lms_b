using lms_b.Middlewares;

namespace lms_b;

public static class LoginEndpoints
{
    public static RouteGroupBuilder MapLoginEndpoints(this WebApplication app)
    {
        RouteGroupBuilder RouteGroup = app.MapGroup("login");

        RouteGroup.MapPost("/", () => {
            Console.WriteLine("Login route");

            return Results.Ok();
        }).AddEndpointFilter(AuthMiddleware.Authenticate);

        return RouteGroup;
    }
}
