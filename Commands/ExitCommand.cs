using System.Windows.Input;
using zipcodeFinder.Database;

namespace zipcodeFinder.Commands
{
    public class ExitCommand : ICustomCommand
    {
        private readonly DatabaseConnection _db;

        public ExitCommand(DatabaseConnection db)
        {
            _db = db;
        }
        public void Execute()
        {
            Console.WriteLine("Exiting application...");
            System.Environment.Exit(0);
        }
    }
}