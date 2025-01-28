using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder.Database;
using zipcodeFinder.Infrastructure;

var host = Host.CreateDefaultBuilder(args);
host.ConfigureServices((context, services) =>
{
    services.AddTransient<DatabaseConnection>();
    services.AddHostedService<Worker>();
});

await host.RunConsoleAsync();
