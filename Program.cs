using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sg =>
{
    sg.SwaggerDoc("v1", new OpenApiInfo {Title = "Schedule API", Version = "v1",});
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