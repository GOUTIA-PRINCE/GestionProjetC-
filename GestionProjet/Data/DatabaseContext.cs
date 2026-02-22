using System.Configuration;
using MySql.Data.MySqlClient;

namespace GestionProjet.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["GestionProjetDB"].ConnectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}