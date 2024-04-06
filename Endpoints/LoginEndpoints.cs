using lms_b.Controllers;
using lms_b.Middlewares;

namespace lms_b.Endpoints;

public static class LoginEndpoints
{
    private static readonly LoginEndpointController Controller = new LoginEndpointController();
    public static RouteGroupBuilder MapLoginEndpoints(this WebApplication app)
    {
        RouteGroupBuilder RouteGroup = app.MapGroup("login");

        RouteGroup.MapPost("/", Controller.LoginController);

        RouteGroup.MapPost("/verify", Controller.VerifyAuthenticationTokenController)
            .AddEndpointFilter(AuthMiddleware.Authenticate);


        return RouteGroup;
    }
}
