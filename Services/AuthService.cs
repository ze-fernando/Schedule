public static class AuthService
{
    public static string? Login(string? email, string? pass)
    {
        using (var context = new AppDbContext())
        {
            var user = context.Users?
                .Where(u => u.Email == email).FirstOrDefault();

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(pass, user.Password))
            {

                return null;
            }

        };

        string token = TokenService.Generate();
        return token;
    }

    public static User Signin(UserDto user)
    {
        var newUser = new User
        {
            FirstName = user.firstName,
            LastName = user.lastName,
            Email = user.email,
            Password = user.password
        };

        using (var context = new AppDbContext())
        {

            context.Users?.Add(newUser);

            context.SaveChanges();
        }

        return newUser;
    }

}
