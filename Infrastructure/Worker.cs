using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder.Database;
using zipcodeFinder.Finder;

namespace zipcodeFinder.Infrastructure
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly DatabaseConnection _db;
        public Worker(IHostApplicationLifetime hostApplicationLifetime, DatabaseConnection db)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _db = db;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Hello, World!");
            CommandHandler commandHandler = new();
            commandHandler.Run();
            
            _hostApplicationLifetime.StopApplication();// stops the application
            return Task.CompletedTask;
        }
    }
}