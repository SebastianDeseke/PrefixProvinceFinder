using Microsoft.Extensions.DependencyInjection;
using zipcodeFinder.Commands;

namespace zipcodeFinder.Infrastructure;

public class CommandHandler
{
    private readonly Dictionary<string, ICustomCommand> _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(IServiceProvider service)
    {
        _services = service;
        _commands = new Dictionary<string, ICustomCommand>()
        {
            { "1", service.GetRequiredService<SearchPrefixCommand>() },
            { "2", service.GetRequiredService<SearchZipcodeCommand>() },
            { "exit", service.GetRequiredService<ExitCommand>() }
        };
    }

    public void Run() {
        while (true)
        {
            Console.WriteLine("Choose an option: \n1. Search for/with prefix\n2. Search for/with zipcode\nexit. Exit the program");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (_commands.ContainsKey(input))
            {
                _commands[input].Execute();
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }
}