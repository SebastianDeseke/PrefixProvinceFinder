using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace zipcodeFinder_firstDraft.ZipCodeFinder
{
    public class PrefixFinder
    {
        public string ExtractPrefix (string telefone)
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
    }
}