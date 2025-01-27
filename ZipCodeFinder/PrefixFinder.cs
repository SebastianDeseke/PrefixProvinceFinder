using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using zipcodeFinder_firstDraft.Database;

namespace zipcodeFinder_firstDraft.ZipCodeFinder
{
    public class PrefixFinder
    {
        private readonly DatabaseConnection _db;

        public PrefixFinder(DatabaseConnection db)
        {
            _db = db;
        }
        public string ExtractPrefix(string telefone)
        {
            string pattern = @"(?:\+49|49|0049|0)(\d{5})"; // first five digits after the country code
            string prefix = "";
            Regex regex = new(pattern);
            Match match = regex.Match(telefone);
            if (match.Success)
            {
                prefix = match.Groups[1].Value;
                Console.WriteLine("Prefix: " + prefix);
            }
            else
            {
                Console.WriteLine("No match found");
            }
            return prefix;
        }

        public void CheckPrefix(string unchecked_prefix)
        {
            bool status = _db.CheckIfPrefixExists(unchecked_prefix);
            if (status)
            {
                Console.WriteLine("5 digit Prefix exists in database");
            }
            else
            {
                Console.WriteLine("5 digit Prefix does not exist in database");
                string fourDigitPrefix = unchecked_prefix.Substring(0, 4);
                status = _db.CheckIfPrefixExists(fourDigitPrefix);
                if (status)
                {
                    Console.WriteLine("4 digit Prefix exists in database");
                }
                else
                {
                    Console.WriteLine("4 digit Prefix does not exist in database. Please enter a valid prefix.");
                }
            }
        }
    }
}