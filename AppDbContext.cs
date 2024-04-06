using lms_b.Dtos;
using lms_b.Dtos.Model;
using lms_b.Utils;
using Microsoft.EntityFrameworkCore;

namespace lms_b;

public class AppDbContext(string ConnectionString) : DbContext, IDisposable
{
    private readonly string ConnectionString = ConnectionString;
    private static AppDbContext? context;

    public static AppDbContext CreateDBContext(string ConnectionString)
    {
        AppDbContext.context = new AppDbContext(ConnectionString);
        return AppDbContext.context;
    }

    public static AppDbContext? GetDBContext()
    {
        return AppDbContext.context;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SyllabusDto>().HasNoKey();
        modelBuilder.Entity<CourseDetailsDto>().HasKey(c => c.CourseId);
        modelBuilder.Entity<SignupRequestDto>().HasKey(c => c.Id);
    }

    // This one is broken
    public bool AddCourse(CourseDetailsDto course)
    {
        if(context == null) return false;

        try {
            context.CourseDetails.Add(course);
            context.SaveChanges();
            return true;
        } catch {

            /*  
                Add a logger for both development and production env 
                to handle exception and msg.
            */
            return false;
        }


    }

    public async Task<Result<bool, string>> AddUser(SignupRequestDto user)
    {
        if(context == null) {
            return Result<bool, string>
                .Err("Sorry, we're experiencing technical difficulties at the moment. Please try again later or contact support if the issue persists.");
        }

        try {

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

        } catch(Exception e) {
            Console.WriteLine(e);
            return Result<bool, string>
                .Err("An error occurred while saving your data. Please try again later.");
        }
        
        return Result<bool, string>.Ok(true);
    }

    public async Task<bool> IsUserCredentialValid(LoginRequestDto userRequest)
    {

        // Hash Password
        var hashedPassword = await Utils.Utils.HashStringValue(userRequest.Password);

        try {
            return await Users
                .AnyAsync(
                    user => user.Email == userRequest.Email && 
                    user.Password == hashedPassword
                );
        } catch {
            return false;
        }

    }
    public DbSet<SignupRequestDto> Users { get; set; }

    public DbSet<CourseDetailsDto> CourseDetails { get; set; }
    public DbSet<QuizDto> Quizzes { get; set; }
    public DbSet<QuizDataDto> QuizData { get; set; }
    public DbSet<SyllabusDto> Syllabus { get; set; }
}
