using lms_b.Dtos;
using lms_b.Utils;

namespace lms_b.Services;

public class RegisterationService
{
    private static readonly AppDbContext DBContext = AppDbContext.GetDBContext() 
        ?? throw new Exception("Database Error: No connection established"); 
    public async static Task<Result<bool, string>> RegisterUser(SignupRequestDto user)
    {
        string hashedPassword = await Utils.Utils.HashPassword(user.Password);

        return await DBContext.AddUser(
            new SignupRequestDto(
                0,
                user.FName,
                user.LName,
                user.Email,
                hashedPassword
            )
        );
    }
}
