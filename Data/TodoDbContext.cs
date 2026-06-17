using Microsoft.EntityFrameworkCore;

namespace SemanticKernelGeminiDemo.Data;


public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}