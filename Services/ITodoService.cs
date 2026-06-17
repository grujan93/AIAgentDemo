using SemanticKernelGeminiDemo.Data;

namespace SemanticKernelGeminiDemo.Services;

public interface ITodoService
{
    Task<TodoItem> AddTaskAsync(string title);
    Task<List<TodoItem>> GetTasksAsync();
    Task<bool> MarkTaskAsDoneAsync(int id);

}