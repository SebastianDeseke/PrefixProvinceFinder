// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder_firstDraft.Database;
using zipcodeFinder_firstDraft.ZipCodeFinder;

var host = Host.CreateDefaultBuilder(args);
host.ConfigureServices((context, services) =>
{
    services.AddTransient<DatabaseConnection>();
    services.AddHostedService<Worker>();
});

await host.RunConsoleAsync();

public class Worker : BackgroundService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    public Worker (IHostApplicationLifetime hostApplicationLifetime)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        PrefixFinder prefixFinder = new PrefixFinder();
        Console.WriteLine("Hello, World!");
        string test = Console.ReadLine();
        prefixFinder.ExtractPrefix(test);
        _hostApplicationLifetime.StopApplication();// stops the application
        return Task.CompletedTask;
    }
}
