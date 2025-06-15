using System.Text.Json.Nodes;

public class TodoItem
{
    public static TodoItem[] testData = new[]
    {
        new TodoItem("1", "Learn ASP.NET Core", true),
        new TodoItem("2", "Build a REST API", false, 1),
        new TodoItem("3", "Deploy to Azure", false, 2)
    };

    public TodoItem(string id, string title, bool completed = false, int order = -1)
    {
        Id = id;
        Title = title;
        Completed = completed;
        Order = order;
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
    public int Order { get; set; }

    public static TodoItem FromJson(JsonObject json)
    {
        return new TodoItem(
            json["id"].GetValue<string>(),
            json["title"].GetValue<string>(),
            json["completed"].GetValue<bool>(),
            json["order"].GetValue<int>()
        );
    }

    public static JsonObject ToJson(TodoItem item)
    {
        return new JsonObject
        {
            ["id"] = item.Id,
            ["title"] = item.Title,
            ["completed"] = item.Completed,
            ["order"] = item.Order
        };
    }
}