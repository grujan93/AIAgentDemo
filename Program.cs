using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SemanticKernelGeminiDemo.Chat;
using SemanticKernelGeminiDemo.Data;
using SemanticKernelGeminiDemo.Endpoints;
using SemanticKernelGeminiDemo.Plugins;
using SemanticKernelGeminiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var googleApiKey = builder.Configuration["GoogleAi:ApiKey"];
var model = builder.Configuration["GoogleAi:Model"] ?? "gemini-3.1-flash-lite";

if (string.IsNullOrWhiteSpace(googleApiKey))
{
    throw new Exception("Google AI Api Key is not set. Add it through dotnet User-Secrets");
}

builder.Services.AddSingleton(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();

    kernelBuilder.AddGoogleAIGeminiChatCompletion(
        modelId: model,
        apiKey: googleApiKey
    );

    kernelBuilder.Plugins.AddFromType<TimePlugin>();
    kernelBuilder.Plugins.AddFromType<CalculatorPlugin>();

    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    kernelBuilder.Plugins.AddFromObject(new TodoPlugin(scopeFactory));

    return kernelBuilder.Build();
});

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlite("Data Source=todos.db");
});

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddSingleton<ChatHistoryStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.MapChatEndpoints();
app.MapTodoEndpoints();

app.Run();

