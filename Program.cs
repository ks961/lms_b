using lms_b;
using lms_b.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string ConnectionString = Environment.GetEnvironmentVariable("DBConn") 
    ?? throw new Exception("Database Connection Failed: No SQL connection string found!");

try {

    AppDbContext.CreateDBContext(ConnectionString);

} catch(Exception e) {
    Console.WriteLine(e);
}

app.MapCoursesEndpoints();

app.MapLoginEndpoints();

app.Run();