using lms_b.Utils;
using MySqlX.XDevAPI;

namespace lms_b.Data;

public record class User(
    string Data,
    string Cookie
) : IValidator<string, string>
{
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

public class LoggedInUsers
{
    private static List<User> LoggedInClients = [
        new User(
            "cmskdmclksdmcd",
            "cookieevalue"
        )
    ];

    public static void AddClient(User User)
    {
        LoggedInClients.Append(User);
    }

    public static void RemoveClient(User User)
    {
        LoggedInClients.RemoveAll(client => client.Cookie == User.Cookie);
    }

    public static bool IsThereAClient(string cookie)
    {
        return LoggedInClients.Any(client => client.Cookie == cookie);
    }


}
