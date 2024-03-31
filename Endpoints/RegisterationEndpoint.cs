using lms_b.Controllers;

namespace lms_b.Endpoints;

public static class RegisterationEndpoints
{

    private static readonly RegistrationController Controller = new RegistrationController(); 
    public static RouteGroupBuilder MapRegisterationEndpoints(this WebApplication app)
    {
        RouteGroupBuilder RouteGroup = app.MapGroup("signup");

        RouteGroup.MapPost("/", Controller.RegisterIndexController);      

        return RouteGroup;
    }
}
