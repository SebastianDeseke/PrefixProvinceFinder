using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using zipcodeFinder.Database;

namespace zipcodeFinder.Finder
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
            string[] prefixesToCheck = { unchecked_prefix, unchecked_prefix.Substring(0, 4) };
            foreach (string prefix in prefixesToCheck)
            {
                if (_db.CheckIfPrefixExists(prefix))
                {
                    Console.WriteLine($"{prefix.Length}-digit prefix {prefix} exists in the database");
                    return;
                }
                Console.WriteLine($"Neither 5-digit nor 4-digit prefix exists in the database. Please enter a valid prefix.");
            }
        }

        public string GetCity(string prefix)
        {
            return _db.GetCity(prefix);
        }

        public string GetProvince(string prefix)
        {
            return _db.GetProvince(prefix);
        }
        
        public string DisplayPrefixesForProvince (string province)
        {
            string result = $"{province} has the following prefixes: \n";
            List<string> prefixes = _db.GetProvincePrefixes(province);
            foreach (var prefix in prefixes)
            {
                result += prefix + "\n";
            }
            return result;
        }
    }
}