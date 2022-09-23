using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal9000Cli
{
    [Command(Name = "todo", Description = "Manage to Todo (Create, Update, and Query)")]
    public class TodoCmd
    {
        protected Task<int> OnExecute(CommandLineApplication app)
        {
            return Task.FromResult(0);
        }
    }
}
