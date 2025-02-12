using Microsoft.Extensions.DependencyInjection;
using zipcodeFinder.Database;
using zipcodeFinder.Finder;

namespace zipcodeFinder.Commands
{
    public class SearchPrefixCommand : ICustomCommand
    {
        private readonly DatabaseConnection _db;

        public SearchPrefixCommand(DatabaseConnection db)
        {
            _db = db;
        }

        //Searches for the prefix given a search paramater
        public void Execute()
        {
            Console.WriteLine("what would you like to search for? \n\tCity \n\tProvince \n\tGet prefixes for province(keyword `prefix`) \n\tFind a prefix based on City or Zipcode (keyword `find`)");
            string search = Console.ReadLine()?.Trim().ToLower();
            PrefixFinder finder = new PrefixFinder(_db);
            switch (search)
            {
                case "city":
                    Console.WriteLine("Enter the prefix you would like to search for");
                    string city = Console.ReadLine();
                    Console.WriteLine(finder.GetCity(city));
                    break;
                case "province":
                    Console.WriteLine("Enter the prefix you would like to search for");
                    string province = Console.ReadLine();
                    Console.WriteLine(finder.GetProvince(province));
                    break;
                case "prefix":
                    Console.WriteLine("Enter the province you would like the prefixes of");
                    string prefix = Console.ReadLine();
                    finder.DisplayPrefixesForProvince(prefix);
                    break;
                case "find":
                    Console.WriteLine("Enter the condition you would like to search with (a.k.a the city or zipcode)");
                    string condition = Console.ReadLine();
                    Console.WriteLine("Enter the value you want to search with");
                    string searchType = Console.ReadLine();
                    finder.GetPrefix(condition,searchType);
                    break;
                default:
                    Console.WriteLine("Invalid search parameter");
                    break;
            }
        }
    }
}