using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using zipcodeFinder.Database;

namespace zipcodeFinder.Finders
{
    public class zipcodeFinder
    {
        private readonly DatabaseConnection _db;

        public zipcodeFinder(DatabaseConnection db)
        {
            _db = db;
        }

        public bool CheckIfZipcodeExists(string zipcode)
        {
            return _db.CheckIfZipcodeExists(zipcode);
        }

        public string GetCity(string zipcode)
        {
            return _db.GetCity(zipcode);
        }
    }
}