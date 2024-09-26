using Microsoft.OpenApi.Models;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapControllers();
        app.UseRouting();
        app.UseCors("CorsPolicy");
        ConfigureAIEndPoints(app);

        app.Run();
    }

    private static void ConfigureAIEndPoints(WebApplication app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // Serve AI plugin manifest
            endpoints.MapGet("/.well-known/ai-plugin.json", async context =>
            {
                context.Response.ContentType = "application/json";
                using var reader = new StreamReader(GetAiPluginManifestFilePath());
                var json = reader.ReadToEnd();
                await context.Response.WriteAsync(json);
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
        return Path.Combine(app, ".well-known/ai-plugin.json");
    }
}
