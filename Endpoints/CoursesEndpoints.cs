using lms_b.Middlewares;
using lms_b.Services;

namespace lms_b.Endpoints;

public static class CoursesEndpoint
{    public static RouteGroupBuilder MapCoursesEndpoints(this WebApplication app)
    {
        var RouteGroup = app.MapGroup("courses");

        RouteGroup.MapGet("/", CourseService.FetchAllCourse)
            .AddEndpointFilter(AuthMiddleware.Authenticate);
        
        RouteGroup.MapGet("/{CourseId}", CourseService.FetchCourseByCourseId)
            .AddEndpointFilter(AuthMiddleware.Authenticate);

        RouteGroup.MapPost("/", CourseService.AddCourse)
            .WithName(CourseService.PostCourseName);

        return RouteGroup;
    }

}
