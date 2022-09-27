using System;
using System.Text.Json;

namespace Hal9000Cli.Todo
{
public class TodoService
{
    private readonly string _databasePath = $"{System.IO.Path.GetTempPath()}/data.json";
    private List<TodoItem> _items = new();

    public TodoService()
    {
        if (!File.Exists(_databasePath))
        {
            File.WriteAllText(_databasePath, "[]");
        }
            
        Refresh();
    }

    public TodoItem AddItem(string title)
    {
        var item = new TodoItem
        {
            Title = title,
            CreatedTime = DateTime.Now,
            Id = _items.Any() ? _items.Max(x => x.Id) + 1 : 1
        };

        _items.Add(item);

        Save();

        return item;
    }

    public TodoItem? UpdateItem(int id, string title)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            Console.WriteLine("Item not found!");
            return null;
        }
        item.Title = title;
        item.UpdatedTime = DateTime.Now;

        Save();

        return item;
    }

    public TodoItem? MarkAsDone(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            Console.WriteLine("Item not found!");
            return null;
        }
        item.Completed = true;
        item.UpdatedTime = DateTime.Now;
        item.CompletedTime = DateTime.Now;

        Save();

        return item;
    }

    public List<TodoItem> GetItems()
    {
        Refresh();
        return _items;
    }

    private void Refresh()
    {
        var data = File.ReadAllText(_databasePath);
        _items = JsonSerializer.Deserialize<List<TodoItem>>(data)!;
        if(_items == null)
        {
            _items = new();
        }
    }

    private void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_items, options);
        File.WriteAllText(_databasePath, jsonString);
    }

    public TodoItem? GetItem(int id)
    {
        Refresh();
        return _items.FirstOrDefault(x => x.Id == id);
    }
}
}

