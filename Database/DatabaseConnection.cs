using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Utilities;
using Microsoft.CSharp;
using MySqlConnector.Logging;
using System.Collections;

namespace zipcodeFinder_firstDraft.Database
{
    public class DatabaseConnection
    {
        private MySqlConnection connection { get; set; }
        private readonly IConfiguration _config;
        private readonly ILogger<DatabaseConnection> _logger;

        public DatabaseConnection(IConfiguration config, ILogger<DatabaseConnection> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void Connect()
        {
            connection = new MySqlConnection(_config["ConnectionStrings:DatabaseConnection"]);
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }

        public List<string> GetProvincePrefixes (string province)
        {
            Connect();
            List<string> provincePrefixes = new();
            List<string> provinceZipcode = new();
            var cmd1 = connection.CreateCommand();
            cmd1.CommandText = $"SELECT Zipcode FROM zipcodes_gr WHERE Province = @province";
            cmd1.Parameters.AddWithValue("@province", province);
            using var reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                provinceZipcode.Add(reader1.GetString(0));
            }
            var cmd2 = connection.CreateCommand();
            cmd2.CommandText = $"SELECT Prefix, Place_Name, Zipcode FROM prefix_zipcode WHERE Zipcode LIKE ";
            using var reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                provincePrefixes.Add(reader.GetString(1));
            }
            Disconnect();
            return provincePrefixes;
        }

        // if I update the database to include the city in zipcodes_gr, I can use this method to get the city
        public string GetCity(string prefix)
        {
            Connect();
            string city = "";
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT City FROM prefix_zipcode WHERE Prefix = {prefix}";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                city = reader.GetString(0);
            }
            Disconnect();
            return city;
        }

        public string GetProvince (string zipcode)
        {
            Connect();
            string province = "";
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT Province FROM zipcodes_gr WHERE Zipcode = {zipcode}";
            // retunrs single coloumn value from the first row of the result set and eliminates the need for reader
            province = cmd.ExecuteScalar()?.ToString() ?? string.Empty;
            Disconnect();
            return province;
        }
    }
}