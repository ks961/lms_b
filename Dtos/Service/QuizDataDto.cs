using lms_b.Utils;

namespace lms_b.Dtos.Service;

public record class QuizDataDto(
    int Id,
    string QuizId,
    string Question,
    string[] Options
) : IValidator<string, string>
{
   public Result<string, string> Validate()
    {
        var properties = typeof(QuizDataDto).GetProperties();
        
        foreach (var property in properties) {
            if(property.GetValue(this) == null) {
                return Result<string, string>.Err($"Property {property.Name} cannot be null");
            }
        }  
        return Result<string, string>.Ok();
    }
}