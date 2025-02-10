using System.Windows.Input;

namespace zipcodeFinder.Commands
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Exiting application...");
            System.Environment.Exit(0);
        }
    }
}