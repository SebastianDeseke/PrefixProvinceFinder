using System;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using zipcodeFinder.Database;

namespace zipcodeFinder.Finders
{
    public class ZipcodeFinder
    {
        private readonly DatabaseConnection _db;

        public ZipcodeFinder(DatabaseConnection db)
        {
            _db = db;
        }

        public bool CheckIfZipcodeExists(string zipcode)
        {
            Console.WriteLine("Checking if zipcode exists in the database...");
            try
            {
                return _db.CheckIfZipcodeExists(zipcode);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Error checking if zipcode exists in the database");
            }

        }

        public string GetCity(string zipcode)
        {
            Console.WriteLine("Getting city from the database...");
            try
            {
                return _db.GetCity(zipcode);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Error getting city from the database");
            }
        }

        public string GetProvince(string zipcode)
        {
            Console.WriteLine("Getting province from the database...");
            try
            {
                return _db.GetProvince(zipcode);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Error getting province from the database");
            }
        }
    }
}