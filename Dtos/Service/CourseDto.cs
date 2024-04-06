using lms_b.Utils;

namespace lms_b.Dtos.Service;

public record class CourseDto(
    string CourseId,
    string Name,
    string Description,
    string Instructor,
    string InstructorBio,
    QuizDto[] Quizzes, // QuizInfo [ fix it at backend and mysql ]
    SyllabusDto[] Syllabus
) : IValidator<string, string>
{
    public Result<string, string> Validate()
    {
        var properties = typeof(CourseDto).GetProperties();

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
