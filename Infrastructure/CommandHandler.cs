using System.Buffers;
using System.Collections.Generic;
using System.Windows.Input;
using zipcodeFinder.Commands;

namespace zipcodeFinder.Infrastructure
{
    public class CommandHandler
    {
        private readonly Dictionary<string, ICustomCommand> _commands;

        public CommandHandler()
        {
            _commands = new Dictionary<string, ICustomCommand>()
            {
                { "1", new SearchPrefixCommand() },
                { "2", new SearchZipcodeCommand() },
                { "exit", new ExitCommand() }
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
}