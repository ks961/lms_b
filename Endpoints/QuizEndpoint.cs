using lms_b.Controllers;
using lms_b.Middlewares;

namespace lms_b.Endpoints;

public static class QuizEndpoint
{
    private readonly static QuizController controller = new QuizController();
    public static RouteGroupBuilder MapQuizEndpoints(this WebApplication app)
    {
        RouteGroupBuilder RouteGroup = app.MapGroup("quiz");

        RouteGroup.MapGet("/{course_id}", controller.GetQuizes);

        RouteGroup.AddEndpointFilter(AuthMiddleware.Authenticate);

        return RouteGroup;
    }
}
