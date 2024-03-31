using lms_b.Dtos;
using lms_b.Services;
using lms_b.Utils;

namespace lms_b.Controllers;

public class RegistrationController
{
    public async Task<IResult> RegisterIndexController(SignupRequestDto user)
    {
        Result<bool, string> result = await RegisterationService.RegisterUser(user);

        return result.IsErr ?
            Results.BadRequest(result.Error) : 
                Results.Ok();
    }
}
