using System;
using Hal9000Cli.Todo;
using McMaster.Extensions.CommandLineUtils;
using DevLab.JmesPath;
using System.Text.Json;

namespace Hal9000Cli
{
    [Command(Name = "list", Description = "Get list of todo items")]
    [HelpOption]
    public class QueryTodoItemCmd
    {
        private readonly IConsole _console;
        private readonly TodoService _todoService;

        public QueryTodoItemCmd(IConsole console, TodoService todoService)
        {
            _console = console;
            _todoService = todoService;
        }

        [Option(CommandOptionType.SingleValue, ShortName = "", LongName = "query", Description = " JMESPath query string. See http://jmespath.org/ for more information and examples.", ShowInHelpText = true, ValueName = "")]
        public string? Query { get; set; }

        protected Task<int> OnExecute(CommandLineApplication app)
        {
            var items = _todoService.GetItems();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(items, options);

            if (!string.IsNullOrEmpty(Query))
            {
                jsonString = JMESPathTransform(jsonString);
                jsonString = JsonSerializer.Serialize(JsonSerializer.Deserialize<object>(jsonString), options);
            }

            _console.WriteLine(jsonString);

            return Task.FromResult(0);
        }

        private string JMESPathTransform(string input)
        {
            if (!string.IsNullOrEmpty(Query))
            {
                var jmes = new JmesPath();
                return jmes.Transform(input, Query);
            }

            return string.Empty;
        }
    }
}

