using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zipcodeFinder_firstDraft.Database;
using zipcodeFinder_firstDraft.ZipCodeFinder;

namespace zipcodeFinder_firstDraft.Infrastructure
{
    public class Worker : BackgroundService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public Worker(IHostApplicationLifetime hostApplicationLifetime)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Hello, World!");
            PrefixFinder prefixFinder = new PrefixFinder();
            string test = Console.ReadLine();
            prefixFinder.ExtractPrefix(test);
            _hostApplicationLifetime.StopApplication();// stops the application
            return Task.CompletedTask;
        }
    }
}