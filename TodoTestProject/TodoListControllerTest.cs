// Controllers/TodoListControllerTest.cs
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using ToDoList.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

public class TodoListControllerTest
{
    private TodoListController CreateController()
    {
        var loggerMock = new Mock<ILogger<TodoListController>>();
        return new TodoListController(loggerMock.Object);
    }

    private void ResetTestData()
    {
        TodoItem.testData = new[]
        {
            new TodoItem("1", "Task 1", false, 1),
            new TodoItem("2", "Task 2", true, 2)
        };
    }

    [Fact]
    public void GetReturnsAllItems()
    {
        ResetTestData();
        var controller = CreateController();

        var result = controller.Get();

        Assert.Equal(2, result.Count());
        foreach (var item in result)
        {
            Assert.IsType<JsonObject>(item);
        }
    }

    [Fact]
    public void PostAddsNewItemReturnsCreatedAtAction()
    {
        ResetTestData();
        var controller = CreateController();

        var result = controller.Post();

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(controller.Get), createdResult.ActionName);
        Assert.Equal(3, TodoItem.testData.Length);
    }

    [Fact]
    public void DeleteRemovesItemReturnsOk()
    {
        ResetTestData();
        var controller = CreateController();

        var result = controller.Delete("1");

        Assert.IsType<OkResult>(result);
        Assert.Single(TodoItem.testData);
        Assert.Equal(TodoItem.testData.FirstOrDefault(item => item.Id == "1"), null);
    }

    [Fact]
    public void PutUpdatesItemReturnsOk()
    {
        ResetTestData();
        var controller = CreateController();
        var updated = new TodoItem("1", "Updated Task", true, 1);
        var json = TodoItem.ToJson(updated);

        var result = controller.Put(json);

        Assert.IsType<OkResult>(result);
        var item = TodoItem.testData.First(i => i.Id == "1");
        Assert.Equal("Updated Task", item.Title);
        Assert.True(item.Completed);
    }
}