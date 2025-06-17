// Controllers/TodoListControllerTest.cs
using Moq;
using Microsoft.Extensions.Logging;
using ToDoList.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

public class TodoListControllerTest
{
    /// <summary>
    /// Creates a new in-memory TodoContext and seeds it with two TodoItem objects for testing.
    /// </summary>
    private TodoContext CreateInMemoryContext()
    {
        var context = new TodoContext();

        context.Database.Delete();

        // Seed test data
        context.Todos.AddRange([
            new TodoItem("1", "Task 1", false, 1),
            new TodoItem("2", "Task 2", true, 2)
        ]);
        context.SaveChanges();

        return context;
    }

    /// <summary>
    /// Creates a TodoListController instance using a mocked logger and the provided TodoContext.
    /// </summary>
    private TodoListController CreateController(TodoContext context)
    {
        var loggerMock = new Mock<ILogger<TodoListController>>();
        return new TodoListController(loggerMock.Object, context);
    }

    /// <summary>
    /// Tests that the Get method returns all TodoItems as JsonObjects.
    /// </summary>
    [Fact]
    public void GetReturnsAllItems()
    {
        var context = CreateInMemoryContext();
        var controller = CreateController(context);

        var result = controller.Get();

        Assert.Equal(2, result.Count());
        foreach (var item in result)
        {
            Assert.IsType<JsonObject>(item);
        }
    }

    /// <summary>
    /// Tests that the Post method adds a new TodoItem and returns a CreatedAtActionResult.
    /// </summary>
    [Fact]
    public void PostAddsNewItemReturnsCreatedAtAction()
    {
        var context = CreateInMemoryContext();
        var controller = CreateController(context);

        var result = controller.Post();

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(controller.Get), createdResult.ActionName);
        Assert.Equal(3, context.Todos.Count());
    }

    /// <summary>
    /// Tests that the Delete method removes a TodoItem by ID and returns OkResult.
    /// </summary>
    [Fact]
    public void DeleteRemovesItemReturnsOk()
    {
        var context = CreateInMemoryContext();
        var controller = CreateController(context);

        var result = controller.Delete("1");

        Assert.IsType<OkResult>(result);
        Assert.Single(context.Todos);
        Assert.Equal(context.Todos.FirstOrDefault(item => item.Id == "1"), null);
    }

    /// <summary>
    /// Tests that the Put method updates an existing TodoItem and returns OkResult.
    /// </summary>
    [Fact]
    public void PutUpdatesItemReturnsOk()
    {
        var context = CreateInMemoryContext();
        var controller = CreateController(context);
        var updated = new TodoItem("1", "Updated Task", true, 1);
        var json = TodoItem.ToJson(updated);

        var result = controller.Put(json);

        Assert.IsType<OkResult>(result);
        var item = context.Todos.First(i => i.Id == "1");
        Assert.Equal("Updated Task", item.Title);
        Assert.True(item.Completed);
    }
}