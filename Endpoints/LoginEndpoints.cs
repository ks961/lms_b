namespace lms_b;

public static class LoginEndpoints
{
    public static RouteGroupBuilder MapLoginEndpoints(this WebApplication app)
    {
        var RouteGroup = app.MapGroup("login");

        app.MapPost("/", () => {
            
        });

        return RouteGroup;
    }
}
