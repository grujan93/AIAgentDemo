

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;
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

    private static async Task<IResult> AskAsync(ChatRequest request, Kernel kernel)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return Results.BadRequest("Message cannot be empty");
        }

        var settings = new GeminiPromptExecutionSettings
        {
            ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions
        };

        var prompt = $"""
            Ti si koristan AI asistent.
            Odgovaraj jednostavno, kratko i jasno.
            Korisnik je junior .NET developer.
            Za informacije i pitanja prvo proveri zvanicnu Microsoft dokumentaciju a ako ne nadjes onda koristi ostale izvore.

            Ako korisnik pita za vreme koristi dostupnu funkciju.
            Nemoj racunati matematiku iz glave ako postoji dostupna funkcija.
            Ako postoji task koji korisnik zeli da doda, vidi ili zavrsi task koristi dostupne todo funkcije.

            Nemoj izmisljati taskove. Ako treba listu taskova, koristi funkciju GetTasks.

            Pitanje korisnika je: {request.Message} 
        """;

        var result = await kernel.InvokePromptAsync(prompt, new KernelArguments(settings));

        return Results.Ok(new ChatResponse
        {
            Answer = result.ToString()
        });
    }
}