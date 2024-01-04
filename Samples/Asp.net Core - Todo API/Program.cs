using Asp.net_Core___Todo_API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
