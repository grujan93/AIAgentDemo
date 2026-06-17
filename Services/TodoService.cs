using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using SemanticKernelGeminiDemo.Data;

namespace SemanticKernelGeminiDemo.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _db;

    public TodoService(TodoDbContext db)
    {
        _db = db;
    }

    public async Task<TodoItem> AddTaskAsync(string title)
    {
        var item = new TodoItem
        {
            Title = title,
            IsDone = false
        };

        _db.TodoItems.Add(item);
        await _db.SaveChangesAsync();

        return item;
    }

    public async Task<List<TodoItem>> GetTasksAsync()
    {
        return await _db.TodoItems
        .OrderBy(x => x.Id)
        .ToListAsync();

    }

    public async Task<bool> MarkTaskAsDoneAsync(int id)
    {
        var item = await _db.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            return false;
        }

        item.IsDone = true;
        await _db.SaveChangesAsync();

        return true;
    }
}