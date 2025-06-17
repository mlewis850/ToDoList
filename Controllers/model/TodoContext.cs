using System.Data.Entity;

public class TodoContext : DbContext
{
    public DbSet<TodoItem> Todos { get; set; }
}