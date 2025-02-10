using zipcodeFinder.Database;
using zipcodeFinder.Finders;

namespace zipcodeFinder.Commands
{
    public class SearchZipcodeCommand : ICommand
    {
        private readonly DatabaseConnection _db;
        //Searches for the zipcode given a search paramater
        public void Execute()
        {
            Console.WriteLine("what would you like to search for? \nCity \nZipcode");
            string search = Console.ReadLine()?.Trim().ToLower();
            ZipcodeFinder finder = new ZipcodeFinder(_db);
            switch (search)
            {
                case "city":
                    Console.WriteLine("Enter the zipcode you would like to search for");
                    string city = Console.ReadLine();
                    Console.WriteLine(finder.GetCity(city));
                    break;
                case "province":
                    Console.WriteLine("Enter the zipcode you would like to search for");
                    string province = Console.ReadLine();
                    Console.WriteLine(finder.GetProvince(province));
                    break;
                default:
                    Console.WriteLine("Invalid search parameter");
                    break;
            }
        }
    }
}