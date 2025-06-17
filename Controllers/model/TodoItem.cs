using System.Text.Json.Nodes;
using System.ComponentModel.DataAnnotations;

public class TodoItem
{
    [Key]
    public string Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
    public int Order { get; set; }


    public TodoItem() { }

    public TodoItem(string id, string title, bool completed = false, int order = -1)
    {
        Id = id;
        Title = title;
        Completed = completed;
        Order = order;
    }

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