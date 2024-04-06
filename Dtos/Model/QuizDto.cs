using lms_b.Utils;

namespace lms_b.Dtos.Model;

public record class QuizDto(
    string Id,
    string CourseId,
    string QuizName,
    bool Attempted
) : IValidator<string, string>
{
    public Result<string, string> Validate()
    {
        var properties = typeof(QuizDto).GetProperties();

        foreach (var property in properties)
        {
            if (property.GetValue(this) == null)
            {
                return Result<string, string>.Err($"Property {property.Name} cannot be null");
            }
        }
        return Result<string, string>.Ok();
    }
}
