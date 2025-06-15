using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ToDoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    // public TodoItem[] TodoItems = new[]
    // {
    //     new TodoItem(1, "Learn ASP.NET Core", true, 1),
    //     new TodoItem(2, "Build a REST API", false, 2),
    //     new TodoItem(3, "Deploy to Azure", false, 3)
    // };

    private readonly ILogger<TodoListController> _logger;
    private Random _rnd = new Random();

    public TodoListController(ILogger<TodoListController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTodoList")]
    public IEnumerable<JsonObject> Get()
    {
        // string connectionString = "datasource=127.0.0.1; port=1433; database=TestDB; username=SA; password=Password1!;";
        // MySqlConnection databaseConnection = new MySqlConnection(connectionString);

        // string myConnectionString = "server=localhost; port=1433; uid=SA; password=Password1!; database=TestDB; SslMode=none;";
        // MySqlConnection myConnection = new MySqlConnection(myConnectionString);
        // try
        // {
        //     myConnection.Open();
        //     // databaseConnection.Open();
        // }
        // catch (Exception error)
        // {
        //     _logger.LogError("Database connection failed: {Error}", error.Message);
        //     // todo leave
        //     return null;
        // }
        // _logger.LogInformation("Database connection established successfully.");

        // string query = "SELECT * FROM Inventory;";
        // MySqlCommand commandDatabase = new MySqlCommand(query, myConnection);

        // MySqlDataReader reader = commandDatabase.ExecuteReader();

        // while (reader.Read())
        // {
        //     _logger.LogInformation(reader.ToString());
        // }

        return TodoItem.testData.Select(item => TodoItem.ToJson(item));
    }

    [HttpPost(Name = "AddTodoItem")]
    public IActionResult Post()
    {
        Guid id = Guid.NewGuid();
        int maxOrder = TodoItem.testData.Length > 0 ? TodoItem.testData.Max(item => item.Order) : 0;
        TodoItem todoItem = new TodoItem(id.ToString(), "New Task", false, maxOrder + 1);
        TodoItem.testData = TodoItem.testData.Append(todoItem).ToArray();
        return CreatedAtAction(nameof(Get), new { id }, todoItem);
    }

    [HttpDelete(Name = "DeleteTodoItem")]
    public IActionResult Delete(string id)
    {
        TodoItem.testData = TodoItem.testData.ToList().Where(item => item.Id != id).ToArray();
        return Ok();
    }

    [HttpPut(Name = "UpdateTodoItem")]
    public IActionResult Put([FromBody] JsonObject json)
    {
        TodoItem newItem = TodoItem.FromJson(json);

        TodoItem.testData = TodoItem.testData.Select(item => item.Id == newItem.Id ? newItem : item).ToArray();

        return Ok();
    }
}
