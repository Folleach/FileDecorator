using FileDecorator;

const string envVariableName = "FILE_DECORATOR_META_PATH";
var metadataPath = Environment.GetEnvironmentVariable(envVariableName)
                  ?? Environment.GetEnvironmentVariable(envVariableName, EnvironmentVariableTarget.User);
if (string.IsNullOrEmpty(metadataPath))
{
    Console.WriteLine($"Set content meta path use env: {envVariableName}");
    return 1;
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IContentService>(c => new FileContentService(metadataPath, c.GetRequiredService<ILogger<FileContentService>>()));
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

await app.RunAsync();
return 0;
