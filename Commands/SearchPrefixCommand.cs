namespace zipcodeFinder.Commands
{
    public class SearchPrefixCommand : ICommand
    {
        //Searches for the prefix given a search paramater
        public void Execute()
        {
            Console.WriteLine("what would you like to search for? \nCity \nZipcode");
        }
    }
}