using lms_b.Utils;
namespace lms_b.Dtos;


public record class CourseDto(
    string CourseId,
    string CourseName,
    string CourseDescription
) : IValidator<string, string>
{
    public Result<string, string> Validate()
    {
        var properties = typeof(CourseDto).GetProperties();
        
        foreach (var property in properties) {
            if(property.GetValue(this) == null) {
                return Result<string, string>.Err($"Property {property.Name} cannot be null");
            }
        }  
        return Result<string, string>.Ok();
    }
}
