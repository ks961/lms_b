using lms_b.Services;

namespace lms_b.Endpoints;

public static class CoursesEndpoint
{    public static RouteGroupBuilder MapCoursesEndpoints(this WebApplication app)
    {
        var RouteGroup = app.MapGroup("courses");

        RouteGroup.MapGet("/", CourseService.FetchAllCourse);
        
        RouteGroup.MapGet("/{CourseId}", CourseService.FetchCourseByCourseId);

        RouteGroup.MapPost("/", CourseService.AddCourse)
            .WithName(CourseService.PostCourseName);

        return RouteGroup;
    }

}
