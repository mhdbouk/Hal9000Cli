using Hal9000Cli;
using Hal9000Cli.Todo;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddScoped<TodoService>()
    .BuildServiceProvider();


var app = new CommandLineApplication<Hal9000Cmd>();
app.Conventions
    .UseDefaultConventions()
    .UseConstructorInjection(services);

app.Execute(args);