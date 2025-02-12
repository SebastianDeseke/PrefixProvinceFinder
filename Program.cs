using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder.Commands;
using zipcodeFinder.Database;
using zipcodeFinder.Infrastructure;

var host = Host.CreateDefaultBuilder(args);
host.ConfigureServices((context, services) =>
{
    services.AddTransient<DatabaseConnection>();
    services.AddTransient<CommandHandler>();
    services.AddTransient<SearchPrefixCommand>();
    services.AddTransient<SearchZipcodeCommand>();
    services.AddTransient<ExitCommand>();
    services.AddHostedService<Worker>();
});

await host.RunConsoleAsync();
