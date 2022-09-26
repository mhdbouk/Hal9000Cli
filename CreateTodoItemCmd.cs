using System;
using System.ComponentModel.DataAnnotations;
using Hal9000Cli.Todo;
using McMaster.Extensions.CommandLineUtils;

namespace Hal9000Cli
{
    [Command(Name = "create", Description = "Create a Todo Item")]
    [HelpOption]
    public class CreateTodoItemCmd
    {
        private readonly IConsole _console;
        private readonly TodoService _todoService;

        public CreateTodoItemCmd(IConsole console, TodoService todoService)
        {
            _console = console;
            _todoService = todoService;
        }

        [Option(CommandOptionType.SingleValue, ShortName = "t", LongName = "title", Description = "The title of the todo item", ShowInHelpText = true, ValueName = "The title of the todo item")]
        [Required]
        public string? Title { get; set; }

        protected Task<int> OnExecute(CommandLineApplication app)
        {
            var item = _todoService.AddItem(Title!);

            _console.WriteLine($"Todo item \"{item.Title}\" with Id {item.Id} created succesfully!");

            return Task.FromResult(0);
        }
    }
}
