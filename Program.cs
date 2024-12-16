using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore; 
using Microsoft.OpenApi.Models;
using Schedule.Services;
using Schedule.Entities;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Schedule.Job;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuartz(qtz => 
{
    qtz.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("SendScheduleJob");

    qtz.AddJob<JobSchedule>(options => options.WithIdentity(jobKey));

    qtz.AddTrigger(options => options
    .ForJob(jobKey)
    .WithIdentity("SendScheduleTrigger")
    .StartNow()
    .WithSimpleSchedule(s => s
        .WithIntervalInSeconds(30)
        .RepeatForever())
    );
});

builder.Services.AddQuartzHostedService(qtz => qtz.WaitForJobsToComplete = true);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
              options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<TokenService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("DataSource=sqlite.db; Cache=Shared"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(sg =>
{
    sg.SwaggerDoc("v1", new OpenApiInfo { Title = "Schedule API", Version = "v1" });

    sg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no formato: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    sg.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(sg =>
    {
        sg.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        sg.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
