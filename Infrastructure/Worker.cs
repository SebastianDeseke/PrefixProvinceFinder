using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder.Database;
using zipcodeFinder.ZipCodeFinder;

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
            PrefixFinder prefixFinder = new PrefixFinder(_db);
            string test = Console.ReadLine();
            Console.WriteLine(_db.GetProvincePrefixes(test));
            //string prefix = prefixFinder.ExtractPrefix(test);
            //prefixFinder.CheckPrefix(prefix);
            _hostApplicationLifetime.StopApplication();// stops the application
            return Task.CompletedTask;
        }
    }
}