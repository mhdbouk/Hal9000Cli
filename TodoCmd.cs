using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal9000Cli
{
    [Command(Name = "todo", Description = "Manage Todo Items (Create, Update, and List)")]
    [Subcommand(
        typeof(CreateTodoItemCmd),
        typeof(UpdateTodoItemCmd),
        typeof(QueryTodoItemCmd)
    )]
    public class TodoCmd
    {
        protected Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return Task.FromResult(0);
        }
    }
}
