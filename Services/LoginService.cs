public static class LoginService
{
    public static string? Login(string email, string pass)
    {
        using (var context = new AppDbContext())
        {
            var user = context.Users?
                .Where(u => u.Email == email).FirstOrDefault();

            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(pass, user.Password))
            {

                string token = TokenService.Generate();
                return token;
            }


        };


        return null;
    }
}
