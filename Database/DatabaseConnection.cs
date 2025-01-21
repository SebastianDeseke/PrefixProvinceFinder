using System.Configuration.Internal;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using Microsoft.CSharp;
using MySqlConnector.Logging;
using System.Collections;
using System.Data;

namespace zipcodeFinder_firstDraft.Database
{
    public class DatabaseConnection
    {
        private MySqlConnection connection { get; set; }
        private readonly IConfiguration _config;
        private readonly ILogger<DatabaseConnection> _logger;

        public DatabaseConnection(IConfiguration config)
        {
            _config = config;
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
    }
}