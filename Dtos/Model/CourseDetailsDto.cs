using lms_b.Utils;
namespace lms_b.Dtos.Model;


public record class CourseDetailsDto(
    // int Id,
    string CourseId,
    string Name,
    string Description,
    string Instructor,
    string InstructorBio
) : IValidator<string, string>
{
    public Result<string, string> Validate()
    {
        var properties = typeof(CourseDetailsDto).GetProperties();

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
