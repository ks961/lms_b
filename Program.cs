using lms_b;
using lms_b.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string ConnectionString = builder.Configuration.GetConnectionString("MySqlConnection") 
    ?? throw new Exception("Database Connection Failed: No SQL connection string found!");

AppDbContext.CreateDBContext(ConnectionString);

app.MapCoursesEndpoints();

app.MapLoginEndpoints();

app.Run();