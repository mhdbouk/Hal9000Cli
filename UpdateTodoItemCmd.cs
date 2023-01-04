using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Hal9000Cli.Todo;
using McMaster.Extensions.CommandLineUtils;

namespace Hal9000Cli;

[Command(Name = "update", Description = "Update a Todo Item")]
[HelpOption]
public class UpdateTodoItemCmd
{ 
    private readonly IConsole _console;
    private readonly TodoService _todoService;

    public UpdateTodoItemCmd(IConsole console, TodoService todoService)
    {
        _console = console;
        _todoService = todoService;
    }

    [Option(CommandOptionType.SingleValue, ShortName = "i", LongName = "id", Description = "The Id of the todo item", ShowInHelpText = true, ValueName = "Integer > 0")]
    [Required]
    public int? Id { get; set; }

    [Option(CommandOptionType.SingleValue, ShortName = "t", LongName = "title", Description = "The title of the todo item", ShowInHelpText = true, ValueName = "String")]
    public string? Title { get; set; }

    [Option(CommandOptionType.NoValue, ShortName = "", LongName = "done", Description = "Update item as Done", ShowInHelpText = true, ValueName = "Mark as Done")]
    public bool MarkAsDone { get; set; }

    protected Task<int> OnExecute(CommandLineApplication app)
    {
        if (MarkAsDone)
        {
            if (!Id.HasValue)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine($"Item Id is required!");
                _console.ResetColor();
                return Task.FromResult(1);
            }

            var item = _todoService.GetItem(Id.Value);
            if (item == null)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine($"Item with Id {Id} not found!");
                _console.ResetColor();
                return Task.FromResult(1);
            }

            item = _todoService.MarkAsDone(Id.Value);
            
            _console.WriteLine($"Item {Id}: \"{item?.Title}\" has been marked as completed at {item?.CompletedTime?.ToString()}");
            return Task.FromResult(0);
        }
        else if (!string.IsNullOrEmpty(Title))
        {
            if (!Id.HasValue)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine($"Item Id is required!");
                _console.ResetColor();
                return Task.FromResult(1);
            }

            var item = _todoService.GetItem(Id.Value);
            if (item == null)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine($"Item with Id {Id} not found!");
                _console.ResetColor();
                return Task.FromResult(1);
            }

            item = _todoService.UpdateItem(Id.Value, Title);

            _console.WriteLine($"Item {Id}: \"{item?.Title}\" has been updated at {item?.UpdatedTime?.ToString()}");
            return Task.FromResult(0);
        }

        _console.BackgroundColor = ConsoleColor.Red;
        _console.WriteLine("You need to specify --done or --title in the update command");
        return Task.FromResult(1);
    }
}

