using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using Witivio.WeatherApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Api", Version = "v1" });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/.well-known/ai-plugin.json", async context =>
{
    await context.Response.WriteAsJsonAsync(new AiPlugin()
    {
        Api = new Api()
        {
            Url = $"{context.Request.Scheme}://{context.Request.Host}/swagger/v1/swagger.json",
        },
        NameForHuman = "Weather forecast Api",
        NameForModel = "Weather forecast Plugin",
        DescriptionForHuman = "Get the weather forecast",
        DescriptionForModel = "Get the weather forecast",
        LogoUrl = $"{context.Request.Scheme}://{context.Request.Host}/logo.webp",
    });
});

app.Run();
