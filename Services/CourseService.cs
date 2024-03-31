using lms_b.Dtos;
using lms_b.Utils;

namespace lms_b.Services;

public class CourseService
{
    /* TODO: Use Dependency Injection */
    private static readonly AppDbContext context = AppDbContext.GetDBContext() 
        ?? throw new Exception("No database connection found.");

    public static readonly string PostCourseName = "PostCourse";

    public static IResult FetchAllCourse()
    {
        return Results.Ok(context.Courses);
    }

    public static IResult FetchCourseByCourseId(string CourseId)
    {
        CourseDto? course = context.Courses
                                .FirstOrDefault(course => course.CourseId == CourseId);

        if(course == null) {
            return Results.NotFound($"Course By Id {CourseId} not found.");
        }   

        return Results.Ok(course);
    }

    public static IResult AddCourse(CourseDto course)
    {
        Result<string, string> result = course.Validate();

        if(result.IsErr) {
            return Results.BadRequest(result.Error);
        }

        bool success = context.AddCourse(course);
        
        return success ?
            Results.CreatedAtRoute(PostCourseName, new {id = course.CourseId}, course) : 
                Results.NoContent();
    }
}
