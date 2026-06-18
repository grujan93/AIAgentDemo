

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using SemanticKernelGeminiDemo.Chat;
using SemanticKernelGeminiDemo.Models;

namespace SemanticKernelGeminiDemo.Endpoints;

public static class ChatEndpoints
{
    private static string Path = "/chat"; 

    public static IEndpointRouteBuilder MapChatEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Path, AskAsync)
            .WithName("AskChat");
        return app;
    }

    private static async Task<IResult> AskAsync(ChatRequest request, Kernel kernel, ChatHistoryStore historyStore)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return Results.BadRequest("Message cannot be empty");
        }

        var sessionId = string.IsNullOrWhiteSpace(request.SessionId) ? "default" : request.SessionId;

        var history = historyStore.GetOrCreate(sessionId);

        history.AddUserMessage(request.Message);

        var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

        var settings = new GeminiPromptExecutionSettings
        {
            ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
        };
        
        var result = await chatCompletionService.GetChatMessageContentAsync(history, settings, kernel);
        var answer = result.Content ?? result.ToString();

        history.AddAssistantMessage(answer); 
        return Results.Ok(new ChatResponse
        {
            Answer = answer
        });
    }
}