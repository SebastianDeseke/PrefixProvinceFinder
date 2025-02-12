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
        private readonly CommandHandler _commandHandler;
        public Worker(IHostApplicationLifetime hostApplicationLifetime, DatabaseConnection db, CommandHandler commandHandler)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _db = db;
            _commandHandler = commandHandler;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Hello, World!");
            _commandHandler.Run();
            
            _hostApplicationLifetime.StopApplication();// stops the application
            return Task.CompletedTask;
        }
    }
}