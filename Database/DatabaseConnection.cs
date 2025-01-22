using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Utilities;
using Microsoft.CSharp;
using MySqlConnector.Logging;
using System.Collections;
using System.Data;
using System.Configuration;

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
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT DISTINCT Province, Zipcode FROM zipcodes WHERE Province LIKE {province}";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                provincePrefixes.Add(reader.GetString(1));
            }
            Disconnect();
            return provincePrefixes;
        }

        // if I update the database to include the city, I can use this method to get the city
        public string GetCity(string zipcode)
        {
            Connect();
            string city = "";
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT City FROM zipcodes WHERE Zipcode = {zipcode}";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                city = reader.GetString(0);
            }
            Disconnect();
            return city;
        }
    }
}