using DotNetEnv;

public static class Settings
{
    public static string SecretKey => Env.GetString("KEY_JWT");

    public static string ServerEmail => Env.GetString("SMTP_EMAIL");
    public static string ServerPassword => Env.GetString("SMTP_PASSWORD");
        
    public static string HostSmtp => Env.GetString("SMTP_HOST");
    public static int PortSmtp => Env.GetInt("SMTP_PORT");

    public static string BaseUrl => Env.GetString("BASE_URI");

    public static string DbConfig => Env.GetString("DATABASE");
}
