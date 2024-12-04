using System.Text.RegularExpressions;

public static class Helpers
{
    public static bool ValidMail(string mail)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(mail, pattern);
    }
}
