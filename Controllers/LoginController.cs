using lms_b.Services;

namespace lms_b.Controllers;

public class LoginEndpointController
{
    public async Task<IResult> LoginController(HttpContext context, LoginRequestDto userRequest)
    {
        /*
            verify user credentails
            if yes:
                generate token
                create cookie string
                set cookie header
                Return response 200 Ok
            else:
                return 401
        */

        if(!await LoginService.IsUserCredentialValid(userRequest)) {
            // context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            // await context.Response.WriteAsync("Error");

            return Results.Unauthorized();
        }

        string cookieString = LoginService.GenerateSessionCookie(userRequest);

        context.Response.Headers.Append("Set-Cookie", cookieString);

        return Results.Ok();
    }


    public IResult VerifyAuthenticationTokenController()
    {
        return Results.Ok();
    }
}
