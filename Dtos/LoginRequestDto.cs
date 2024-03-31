using lms_b.Utils;

namespace lms_b;

public record class LoginRequestDto(
    string Email,
    string Password
) : IValidator<string, string>
{
    public Result<string, string> Validate()
    {
         var properties = typeof(LoginRequestDto).GetProperties();
        
        foreach (var property in properties) {
            if(property.GetValue(this) == null) {
                return Result<string, string>
                            .Err($"Property {property.Name} cannot be null");
            }
        }  
        return Result<string, string>.Ok();
    }
}
