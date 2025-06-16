using Xunit;
using System.Text.Json.Nodes;

public class TodoItemTest
{
    [Fact]
    public void ConstructorSetsPropertiesCorrectly()
    {
        var item = new TodoItem("42", "Test Task", true, 7);

        Assert.Equal("42", item.Id);
        Assert.Equal("Test Task", item.Title);
        Assert.True(item.Completed);
        Assert.Equal(7, item.Order);
    }

    [Fact]
    public void FromJsonCreatesTodoItemFromJsonObject()
    {
        var json = new JsonObject
        {
            ["id"] = "99",
            ["title"] = "FromJson Task",
            ["completed"] = true,
            ["order"] = 5
        };

        var item = TodoItem.FromJson(json);

        Assert.Equal("99", item.Id);
        Assert.Equal("FromJson Task", item.Title);
        Assert.True(item.Completed);
        Assert.Equal(5, item.Order);
    }

    [Fact]
    public void ToJsonCreatesJsonObjectFromTodoItem()
    {
        var item = new TodoItem("100", "ToJson Task", false, 3);

        var json = TodoItem.ToJson(item);

        Assert.Equal("100", json["id"].GetValue<string>());
        Assert.Equal("ToJson Task", json["title"].GetValue<string>());
        Assert.False(json["completed"].GetValue<bool>());
        Assert.Equal(3, json["order"].GetValue<int>());
    }
}