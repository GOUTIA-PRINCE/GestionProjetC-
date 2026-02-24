// =============================================================================
// Contexte de base de données - Couche d'accès aux données (DAL)
// Cette classe encapsule la chaîne de connexion MySQL et fournit une méthode
// pour obtenir une connexion ouvrable vers la base de données.
// =============================================================================

using System.Configuration;
using MySql.Data.MySqlClient;

namespace GestionProjet.Data
{
    /// <summary>
    /// Fournit l'accès à la base de données MySQL via une connexion configurée
    /// dans le fichier App.config (clé "GestionProjetDB").
    /// </summary>
    public class DatabaseContext
    {
        /// <summary>Chaîne de connexion lue depuis App.config.</summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="DatabaseContext"/>
        /// en lisant la chaîne de connexion depuis la configuration de l'application.
        /// </summary>
        public DatabaseContext()
        {
            // Lecture de la chaîne de connexion depuis App.config
            // La clé "GestionProjetDB" doit être définie dans connectionStrings
            _connectionString = ConfigurationManager.ConnectionStrings["GestionProjetDB"].ConnectionString;
        }

        /// <summary>
        /// Crée et retourne une nouvelle instance de <see cref="MySqlConnection"/>
        /// configurée avec la chaîne de connexion de l'application.
        /// Remarque : la connexion n'est PAS ouverte ici, c'est au code appelant
        /// d'appeler <c>Open()</c> avant de l'utiliser.
        /// </summary>
        /// <returns>Une connexion MySQL non ouverte.</returns>
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}