public class User
{
    private string? FirstName { get; set; }
    private string? LastName { get; set; }
    private string? Email { get; set; }
    private string? Password { get; set; }

    public User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
}
