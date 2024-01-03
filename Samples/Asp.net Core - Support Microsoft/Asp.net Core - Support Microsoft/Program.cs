using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerUI;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        services.AddControllers();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast Plugin", Version = "v1" });
            swagger.MapType<JToken>(() => new OpenApiSchema { Type = "object" });
        });
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast");
                options.DocumentTitle = "WeatherForecast Plugin API Documentation";
                options.DocExpansion(DocExpansion.None);
            });
        }
        else
        {
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // Serve AI plugin manifest
            endpoints.MapGet("/.well-known/ai-plugin.json", async context =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.SendFileAsync(GetAiPluginManifestFilePath());
            });
            // Serve OpenAPI spec
            endpoints.MapGet("/openapi.json", async context =>
            {
                context.Response.ContentType = "application/json";
                using var reader = new StreamReader(GetJsonCommentsFilePath());
                var yaml = reader.ReadToEnd();
                await context.Response.WriteAsync(yaml);
            });
        });
    }

    private static string GetJsonCommentsFilePath()
    {
        var app = AppContext.BaseDirectory;
        return Path.Combine(app, $"openapi.json");
    }

    private static string GetAiPluginManifestFilePath()
    {
        var app = AppContext.BaseDirectory;
        return Path.Combine(app, "ai-plugin.json");
    }
}
