using System.Data.Entity.Migrations;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
namespace ToDoList.Controllers;

/// <summary>
/// Controller for managing Todo list items via REST API endpoints.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ILogger<TodoListController> _logger;
    private readonly TodoContext db;

    /// <summary>
    /// Initializes a new instance of the TodoListController.
    /// Accepts a logger and an optional TodoContext (for testing or dependency injection).
    /// </summary>
    public TodoListController(ILogger<TodoListController> logger, TodoContext context = null)
    {
        _logger = logger;
        db = context ?? new TodoContext();
    }

    /// <summary>
    /// Gets all todo items from the database, ordered by their 'Order' property.
    /// Returns each item as a JsonObject.
    /// </summary>
    [HttpGet(Name = "GetTodoList")]
    public IEnumerable<JsonObject> Get()
    {
        var query = from t in db.Todos
                    orderby t.Order
                    select t;

        return query.ToArray().Select(TodoItem.ToJson);
    }

    /// <summary>
    /// Adds a new todo item to the database with default values.
    /// Returns a CreatedAtActionResult with the new item.
    /// </summary>
    [HttpPost(Name = "AddTodoItem")]
    public IActionResult Post()
    {
        var query = from t in db.Todos
                    orderby t.Order
                    select t;
        Guid id = Guid.NewGuid();
        int maxOrder = query.Count() > 0 ? query.Max(item => item.Order) : 0;
        TodoItem newItem = db.Todos.Create();
        newItem.Title = "New task";
        TodoItem todoItem = new TodoItem(id.ToString(), "New Task", false, maxOrder + 1);
        db.Todos.Add(todoItem);
        db.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id }, todoItem);
    }

    /// <summary>
    /// Deletes a todo item from the database by its ID.
    /// Returns OkResult on success.
    /// </summary>
    [HttpDelete(Name = "DeleteTodoItem")]
    public IActionResult Delete(string id)
    {
        db.Todos.RemoveRange(db.Todos.Where(item => item.Id == id));
        db.SaveChanges();

        return Ok();
    }

    /// <summary>
    /// Updates an existing todo item in the database using data from a JsonObject.
    /// Returns OkResult on success.
    /// </summary>
    [HttpPut(Name = "UpdateTodoItem")]
    public IActionResult Put([FromBody] JsonObject json)
    {
        TodoItem newItem = TodoItem.FromJson(json);

        db.Todos.AddOrUpdate(newItem);
        db.SaveChanges();

        return Ok();
    }
}
