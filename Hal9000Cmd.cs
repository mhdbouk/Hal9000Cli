using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hal9000Cli;

[Command(Name = "hal9000", OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
[VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
[Subcommand(typeof(TodoCmd))]
public class Hal9000Cmd
{
    protected Task<int> OnExecute(CommandLineApplication app)
    {
        var displayTitle = new WenceyWang.FIGlet.AsciiArt("HAL9000");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(displayTitle.ToString());
        Console.ResetColor();
        Console.WriteLine();

        app.ShowHelp();
        return Task.FromResult(0);
    }

    private static string? GetVersion()
    => typeof(Hal9000Cmd).Assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
}
