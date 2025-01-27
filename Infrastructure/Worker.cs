using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder_firstDraft.Database;
using zipcodeFinder_firstDraft.ZipCodeFinder;

namespace zipcodeFinder_firstDraft.Infrastructure
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
            PrefixFinder prefixFinder = new PrefixFinder(_db);
            string test = Console.ReadLine();
            string prefix = prefixFinder.ExtractPrefix(test);
            prefixFinder.CheckPrefix(prefix);
            _hostApplicationLifetime.StopApplication();// stops the application
            return Task.CompletedTask;
        }
    }
}