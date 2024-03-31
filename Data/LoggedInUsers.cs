using lms_b.Utils;

namespace lms_b.Data;

public record class User(
    string Cookie,
    DateTime LastLoginTime // UTC TIMESTAMP
) : IValidator<string, string>
{
    public User(string cookie) : this(cookie, DateTime.UtcNow) { }

    public Result<string, string> Validate()
    {
        var properties = typeof(User).GetProperties();

        foreach (var property in properties) {
            if(property.GetValue(this) == null) {
                return Result<string, string>.Err($"User: Property ${property.Name} cannot be null");
            }
        }

        return Result<string, string>.Ok();
    }
}

public class ActiveSessionTracker
{
    private static readonly List<User> ActiveUsers = [];

    public static void AddClient(User User)
    {
        _ = ActiveUsers.Append(User);
    }

    public static void RemoveClient(User User)
    {
        ActiveUsers.RemoveAll(client => client.Cookie == User.Cookie);
    }

    public static bool IsThereAClient(string cookie)
    {
        return ActiveUsers.Any(client => client.Cookie == cookie);
    }


}
