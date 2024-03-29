using lms_b.Dtos;
using lms_b.Utils;

namespace lms_b.Services;

public class LoginService
{
    private static readonly UserDto[] LoggedInClients = [];
    public static bool Login(UserDto user)
    {
        Result<string, string> result = user.Validate();
        if(result.IsErr) {
            return false;
        }

        return true;
    }
}
