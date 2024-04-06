using lms_b.Utils;

namespace lms_b.Dtos.Service;

public record class SyllabusDto(
    int UnitNumber,
    string CourseId,
    string UnitName,
    string[] UnitTopics
) : IValidator<string, string>
{
    public Result<string, string> Validate()
    {
        var properties = typeof(SyllabusDto).GetProperties();

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