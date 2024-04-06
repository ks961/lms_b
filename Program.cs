using lms_b;
using lms_b.Dtos;
using lms_b.Endpoints;
using lms_b.Utils;

var builder = WebApplication.CreateBuilder(args);

var ApiCorsPolicy = "myApiPolicy";

builder.Services.AddCors(
    option => option.AddPolicy(
        name: ApiCorsPolicy,
        builder => {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader();
        }
    )
);

var app = builder.Build();

app.UseCors(ApiCorsPolicy);

string ConnectionString = Environment.GetEnvironmentVariable("DBConn") 
    ?? throw new Exception("Database Connection Failed: No SQL connection string found!");

try {

    AppDbContext.CreateDBContext(ConnectionString);

    // var course = db.CourseDetails.First();
    // // Console.WriteLine(quiz);
    
    // var quizzes = db.Quizzes.First(quiz => quiz.CourseId == course.CourseId);

    // var quizData = db.QuizData.Where(quizData => quizData.QuizId == quizzes.Id).ToList();

    // foreach (var quiz in quizData) {
    //     Console.WriteLine(quiz);
    // }

} catch(Exception e) {
    Console.WriteLine(e);
}



app.MapQuizEndpoints();
app.MapLoginEndpoints();
app.MapCoursesEndpoints();
app.MapRegisterationEndpoints();

app.Run();
