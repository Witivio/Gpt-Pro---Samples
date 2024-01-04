# ASP.NET CORE SAMPLE

### Microsoft Support

This web API sample demonstrates creating a plugin for GTP Pro using an ASP.NET Core Web API.

##### Required Tools

- Visual Studio or Visual Studio Code

##### Procedure to Create the API

1. Create an "ASP.NET Core Web API" project.
2. In this example, create your API controller named "MicrosoftSupportQuestion.cs" in the "controllers" folder with your endpoint.
3. At the root of your project, create the following items:
   - Folder named ".well-known"
   - New file "ai-plugin.json" within the folder
4. Create the file named "openapi.json" at the root of your project to describe the plugin. You can follow the [OpenAPI Specification](https://swagger.io/specification/) to learn how to describe your plugin.
