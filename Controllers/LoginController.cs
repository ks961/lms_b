using lms_b.Dtos;
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

            var failedResponse = new ResponseDto(
                "Authentication Failure: Login Failed, Invalid Credentials",
                false
            );

            return Results.Ok(failedResponse);
        }

        try {

            string cookieString = LoginService.GenerateSessionCookie(userRequest);
            context.Response.Headers.Append("Set-Cookie", cookieString);

            var successResponse = new ResponseDto(
                "Authentication Success: Login Successfull",
                true
            );

            return Results.Ok(successResponse);
        } catch {

            var failedResponse = new ResponseDto(
                "Authentication Failure: Something went wrong!",
                false
            );

            return Results.Ok(failedResponse);

        }


    }


    public IResult VerifyAuthenticationTokenController()
    {
        return Results.Ok();
    }
}
