using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ILogger<TodoListController> _logger;

    public TodoListController(ILogger<TodoListController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTodoList")]
    public IEnumerable<string> Get()
    {
        return new[] { "Todo 1", "Todo 2", "Todo 3" };
    }

    [HttpPost(Name = "PostTodoList")]
    public IActionResult Post([FromBody] string todoItem)
    {
        // Logic to add the todo item
        return CreatedAtAction(nameof(Get), new { id = 1 }, todoItem);
    }
}
