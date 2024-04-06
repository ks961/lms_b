using lms_b.Dtos.Service;

// using lms_b.Dtos.Service;
using lms_b.Utils;
using Microsoft.EntityFrameworkCore;

namespace lms_b.Services;

public class CourseService
{
    /* TODO: Use Dependency Injection */
    private static readonly AppDbContext context = AppDbContext.GetDBContext() 
        ?? throw new Exception("No database connection found.");

    public static readonly string PostCourseName = "PostCourse";

    public static IResult FetchAllCourse()
    {
        return Results.Ok(context.CourseDetails);
    }

    public static async Task<IResult> FetchCourseByCourseId(string CourseId)
    {
        Dtos.Model.CourseDetailsDto? course = await context.CourseDetails
                                .FirstOrDefaultAsync(course => course.CourseId == CourseId);

        if(course == null) {
            return Results.NotFound($"Course By Id {CourseId} not found.");
        }

        var quizzes = await context.Quizzes.Where(quiz => quiz.CourseId == course.CourseId).ToArrayAsync();

        List<QuizDto> quiz = [];

        foreach (var item in quizzes)
        {
            var data = await context.QuizData
                .Where(quizData => quizData.QuizId == item.Id)
                .Select(quizData => new QuizDataDto(
                    quizData.Id,
                    quizData.QuizId,
                    quizData.Question,
                    quizData.Options.Split(",", StringSplitOptions.RemoveEmptyEntries)
                )).ToArrayAsync();

            var quizObj = new QuizDto(
                item.Id,
                item.QuizName,
                item.CourseId,
                item.Attempted,
                data
            );

            quiz.Add(quizObj);
        }

        // TODO: Use a Mapper. ( Make sure each dto should be reponsible for its own conversion b/w layers)
        SyllabusDto[] syllabus = await context.Syllabus
            .Where(syllabus => syllabus.CourseId == CourseId)
            .Select(syllabus => new SyllabusDto(
                syllabus.UnitNumber,
                syllabus.CourseId,
                syllabus.UnitName,
                syllabus.UnitTopics.Split(",", StringSplitOptions.RemoveEmptyEntries)
            )).ToArrayAsync();

        CourseDto courseDetail = new CourseDto(
            course.CourseId,
            course.Name,
            course.Description,
            course.Instructor,
            course.InstructorBio,
            [..quiz],
            syllabus
        );

        return Results.Ok(courseDetail);
    }

    public static IResult AddCourse(CourseDto course)
    {
        Result<string, string> result = course.Validate();

        if(result.IsErr) {
            return Results.BadRequest(result.Error);
        }

        // bool success = context.AddCourse(course);

        bool success = false; // Fix here
        
        return success ?
            Results.CreatedAtRoute(PostCourseName, new {id = course.CourseId}, course) : 
                Results.NoContent();
    }
}
