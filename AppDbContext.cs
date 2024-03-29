using lms_b.Dtos;
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
        modelBuilder.Entity<CourseDto>().HasKey(c => c.CourseId);
    }

    public bool AddCourse(CourseDto course)
    {
        if(context == null) return false;

        try {
            context.Courses.Add(course);
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

    public bool Login(UserDto user)
    {
        if(context == null) return false;

        try {
            Users.Add(user);
        } catch {
            return false;
        }
        
        return true;
    }

    public DbSet<CourseDto> Courses { get; set; }
    public DbSet<UserDto> Users { get; set; }
}
