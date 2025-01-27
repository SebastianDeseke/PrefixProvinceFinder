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

        public List<string> GetProvincePrefixes(string province)
        {
            Connect();
            List<string> provincePrefixes = new();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT DISTINCT Prefix
                                    FROM prefix_zipcode
                                    WHERE Zipcode IN (
                                        SELECT Zipcode
                                        FROM zipcodes_gr
                                        WHERE Province = @province
                                    )";
                cmd.Parameters.AddWithValue("@province", province);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    provincePrefixes.Add(reader.GetString(0));
                }
            }
            finally
            {
                Disconnect();
            }
            return provincePrefixes;
        }

        // if I update the database to include the city in zipcodes_gr, I should maybe change this
        public string GetCity(string prefix)
        {
            Connect();
            string city = "";
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT City FROM prefix_zipcode WHERE Prefix = @prefix";
            cmd.Parameters.AddWithValue("@prefix", prefix);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                city = reader.GetString(0);
            }
            Disconnect();
            return city;
        }

        public string GetProvince(string zipcode)
        {
            Connect();
            string province = "";
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT Province FROM zipcodes_gr WHERE Zipcode = @zipcode";
            cmd.Parameters.AddWithValue("@zipcode", zipcode);
            // retunrs single coloumn value from the first row of the result set and eliminates the need for reader
            province = cmd.ExecuteScalar()?.ToString() ?? string.Empty;
            Disconnect();
            return province;
        }

        public bool CheckIfPrefixExists(string prefix)
        {
            Connect();
            bool exists = false;
            var cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT COUNT(*) FROM prefix_zipcode WHERE Prefix = @prefix";
            cmd.Parameters.AddWithValue("@prefix", prefix);
            if (!cmd.ExecuteScalar().Equals(null))
            {
                exists = true;
            }
            Disconnect();
            return exists;
        }
    }
}