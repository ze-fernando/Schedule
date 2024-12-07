public static class AuthService
{
    public static string? Login(string email, string? pass)
    {
        User? user;
        using (var context = new AppDbContext())
        {
            user = context.Users?.FirstOrDefault(u => u.Email == email);
        }
        if (user == null)
            return null;
        else if (!BCrypt.Net.BCrypt.Verify(pass, user.Password))
            return null;

        else
        {
            var token = TokenService.Generate(email);

            return token;
        }

    }

    public static User Signin(UserDto user)
    {
        var newUser = new User
        {
            FirstName = user.firstName,
            LastName = user.lastName,
            Email = user.email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.password, 12)
        };

        using (var context = new AppDbContext())
        {

            context.Users?.Add(newUser);

            context.SaveChanges();
        }

        return newUser;
    }

}
