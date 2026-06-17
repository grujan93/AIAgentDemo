using SemanticKernelGeminiDemo.Models;
using SemanticKernelGeminiDemo.Services;

namespace SemanticKernelGeminiDemo.Endpoints;

public static class TodoEndpoints
{
    private static readonly string Path = "/todos";

    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(Path)
            .WithTags("TodoEndpoints");

        group.MapGet("/", GetTodosAsync)
            .WithName("GetTodos");

        group.MapPost("/", CreateTodoAsync)
            .WithName("CreateTodo");

        group.MapPut("/{id:int}/done", MarkTodoAsDoneAsync)
            .WithName("MarkTodoAsDone");

        return app;
    }

    private static async Task<IResult> GetTodosAsync(ITodoService todoService)
    {
        var tasks = await todoService.GetTasksAsync();

        return Results.Ok(tasks);
    }

    private static async Task<IResult> CreateTodoAsync(
        CreateTodoRequest request,
        ITodoService todoService)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return Results.BadRequest("Title ne sme biti prazan.");
        }

        var task = await todoService.AddTaskAsync(request.Title);

        return Results.Created($"{Path}/{task.Id}", task);
    }

    private static async Task<IResult> MarkTodoAsDoneAsync(
        int id,
        ITodoService todoService)
    {
        var success = await todoService.MarkTaskAsDoneAsync(id);

        if (!success)
        {
            return Results.NotFound($"Task sa ID {id} nije pronađen.");
        }

        return Results.Ok($"Task sa ID {id} je označen kao završen.");
    }
}