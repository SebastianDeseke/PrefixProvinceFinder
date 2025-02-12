using zipcodeFinder.Database;
using zipcodeFinder.Finder;
using zipcodeFinder.Finders;

namespace zipcodeFinder.Commands
{
    public class SearchZipcodeCommand : ICustomCommand
    {
        private readonly DatabaseConnection _db;

        public SearchZipcodeCommand(DatabaseConnection db)
        {
            _db = db;
        }
        //Searches for the zipcode given a search paramater
        public void Execute()
        {
            Console.WriteLine("what would you like to search for? \nCity \nPrefix \nProvince");
            string search = Console.ReadLine()?.Trim().ToLower();
            ZipcodeFinder finderz = new ZipcodeFinder(_db);
            PrefixFinder finderp = new PrefixFinder(_db); //to get the prefix
            switch (search)
            {
                case "city":
                    Console.WriteLine("Enter the zipcode you would like to search for");
                    string city = Console.ReadLine();
                    Console.WriteLine(finderz.GetCity(city));
                    break;
                case "prefix":
                    Console.WriteLine("Please select the type of search you would like to perform: \n1. Search by city\n2. Search by zipcode");
                    string condition = Console.ReadLine()?.Trim().ToLower();
                    Console.WriteLine("Enter the value you would like to search with");
                    string searchType = Console.ReadLine()?.Trim().ToLower();
                    Console.WriteLine(finderp.GetPrefix(condition, searchType));
                    break;
                default:
                    Console.WriteLine("Invalid search parameter");
                    break;
            }
        }
    }
}