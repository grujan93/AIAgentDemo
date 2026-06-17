
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using SemanticKernelGeminiDemo.Data;
using SemanticKernelGeminiDemo.Services;

namespace SemanticKernelGeminiDemo.Plugins;

public class TodoPlugin
{
     private readonly IServiceScopeFactory _scopeFactory;

    public TodoPlugin(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    [KernelFunction]
    [Description("Dodaje novi todo task u listu.")]
    public async Task<string> AddTask(
        [Description("Naziv taska koji treba dodati.")] string title)
    {
        using var scope = _scopeFactory.CreateScope();

        var todoService = scope.ServiceProvider.GetRequiredService<ITodoService>();

        var item = await todoService.AddTaskAsync(title);
        return $"Task je dodat. ID: {item.Id}, naziv: {item.Title}";
    }

    [KernelFunction]
    [Description("Vraća sve todo taskove iz liste.")]
    public async Task<string> GetTasks()
    {
        using var scope = _scopeFactory.CreateScope();

        var todoService = scope.ServiceProvider.GetRequiredService<ITodoService>();
        
        var tasks = await todoService.GetTasksAsync();

        if (tasks.Count == 0)
        {
            return "Nema taskova.";
        }

        return string.Join(Environment.NewLine, tasks.Select(task =>
        {
            var status = task.IsDone ? "završen" : "nije završen";
            return $"{task.Id}. {task.Title} - {status}";
        }));
    }

    [KernelFunction]
    [Description("Označava todo task kao završen na osnovu njegovog ID-ja.")]
    public async Task<string> MarkTaskAsDone(
        [Description("ID taska koji treba označiti kao završen.")] int id)
    {
        using var scope = _scopeFactory.CreateScope();

        var todoService = scope.ServiceProvider.GetRequiredService<ITodoService>();

        var success = await todoService.MarkTaskAsDoneAsync(id);

        if (!success)
        {
            return $"Task sa ID {id} nije pronađen.";
        }

        return $"Task sa ID {id} je označen kao završen.";
    }
}