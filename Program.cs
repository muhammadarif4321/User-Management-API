using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// middleware custom
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
