// =============================================================================
// Repository Utilisateur - Implémentation concrète de IUtilisateurRepository
// Effectue toutes les opérations SQL CRUD sur la table "utilisateurs" ainsi que
// l'authentification via comparaison de hash SHA-256 géré côté MySQL.
// =============================================================================

using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Data;
using MySql.Data.MySqlClient;

namespace GestionProjet.Repositories
{
    /// <summary>
    /// Implémentation concrète du repository utilisateur.
    /// Interagit directement avec la base de données MySQL via ADO.NET.
    /// Le mot de passe est haché avec SHA2 (256 bits) directement dans les requêtes SQL.
    /// </summary>
    public class UtilisateurRepository : IUtilisateurRepository
    {
        /// <summary>Contexte de base de données fournissant les connexions MySQL.</summary>
        private readonly DatabaseContext _context;

        /// <summary>
        /// Initialise le repository en créant une instance du contexte de base de données.
        /// </summary>
        public UtilisateurRepository()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retourne la liste de tous les utilisateurs actifs, triés alphabétiquement.
        /// Le mot de passe n'est pas récupéré pour des raisons de sécurité.
        /// </summary>
        /// <returns>Liste des utilisateurs actifs.</returns>
        public List<Utilisateur> GetAll()
        {
            var utilisateurs = new List<Utilisateur>();

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // Sélectionne uniquement les utilisateurs actifs (est_actif = true)
                // Le mot de passe est exclu volontairement de cette requête
                string query = "SELECT id, nom, email, date_creation, est_actif, role FROM utilisateurs WHERE est_actif = true ORDER BY nom";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        utilisateurs.Add(new Utilisateur
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("nom"),
                            Email = reader.GetString("email"),
                            DateCreation = reader.GetDateTime("date_creation"),
                            EstActif = reader.GetBoolean("est_actif"),
                            // Si le rôle est NULL en base, on affecte "User" par défaut
                            Role = reader.IsDBNull(reader.GetOrdinal("role")) ? "User" : reader.GetString("role")
                        });
                    }
                }
            }

            return utilisateurs;
        }

        /// <summary>
        /// Recherche un utilisateur par son identifiant unique.
        /// Le mot de passe n'est pas retourné.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur.</param>
        /// <returns>L'utilisateur correspondant ou null s'il n'existe pas.</returns>
        public Utilisateur GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, nom, email, date_creation, est_actif, role FROM utilisateurs WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    // Paramètre préparé pour éviter les injections SQL
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Utilisateur
                            {
                                Id = reader.GetInt32("id"),
                                Nom = reader.GetString("nom"),
                                Email = reader.GetString("email"),
                                DateCreation = reader.GetDateTime("date_creation"),
                                EstActif = reader.GetBoolean("est_actif"),
                                Role = reader.IsDBNull(reader.GetOrdinal("role")) ? "User" : reader.GetString("role")
                            };
                        }
                    }
                }
            }
            // Retourne null si aucun utilisateur n'est trouvé
            return null;
        }

        /// <summary>
        /// Recherche un utilisateur par son adresse email.
        /// Inclut le mot de passe haché pour vérification interne (ex : authentification).
        /// </summary>
        /// <param name="email">Email de l'utilisateur recherché.</param>
        /// <returns>L'utilisateur correspondant (avec hash du mot de passe) ou null.</returns>
        public Utilisateur GetByEmail(string email)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // Ici on récupère aussi le mot_de_passe (haché) pour permettre la vérification
                string query = "SELECT id, nom, email, mot_de_passe, date_creation, est_actif, role FROM utilisateurs WHERE email = @email";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Utilisateur
                            {
                                Id = reader.GetInt32("id"),
                                Nom = reader.GetString("nom"),
                                Email = reader.GetString("email"),
                                MotDePasse = reader.GetString("mot_de_passe"),
                                DateCreation = reader.GetDateTime("date_creation"),
                                EstActif = reader.GetBoolean("est_actif"),
                                Role = reader.IsDBNull(reader.GetOrdinal("role")) ? "User" : reader.GetString("role")
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Insère un nouvel utilisateur en base de données.
        /// Le mot de passe est haché avec SHA2(256) directement dans la requête SQL
        /// pour ne jamais stocker le mot de passe en clair.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à insérer.</param>
        public void Add(Utilisateur utilisateur)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // SHA2(@motDePasse, 256) : hachage du mot de passe côté MySQL
                string query = @"INSERT INTO utilisateurs (nom, email, mot_de_passe, date_creation, est_actif, role) 
                               VALUES (@nom, @email, SHA2(@motDePasse, 256), @dateCreation, @estActif, @role)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", utilisateur.Nom);
                    cmd.Parameters.AddWithValue("@email", utilisateur.Email);
                    cmd.Parameters.AddWithValue("@motDePasse", utilisateur.MotDePasse);
                    cmd.Parameters.AddWithValue("@dateCreation", DateTime.Now);
                    cmd.Parameters.AddWithValue("@estActif", true);
                    // Si le rôle n'est pas spécifié, on attribue "User" par défaut
                    cmd.Parameters.AddWithValue("@role", string.IsNullOrEmpty(utilisateur.Role) ? "User" : utilisateur.Role);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Met à jour le nom, l'email, le statut actif et le rôle d'un utilisateur existant.
        /// Le mot de passe n'est PAS modifié ici (opération séparée).
        /// </summary>
        /// <param name="utilisateur">L'utilisateur avec les nouvelles valeurs.</param>
        public void Update(Utilisateur utilisateur)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE utilisateurs 
                               SET nom = @nom, email = @email, est_actif = @estActif, role = @role 
                               WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@nom", utilisateur.Nom);
                    cmd.Parameters.AddWithValue("@email", utilisateur.Email);
                    cmd.Parameters.AddWithValue("@estActif", utilisateur.EstActif);
                    cmd.Parameters.AddWithValue("@role", string.IsNullOrEmpty(utilisateur.Role) ? "User" : utilisateur.Role);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Effectue un soft delete de l'utilisateur : son compte est désactivé
        /// (est_actif = false) mais l'enregistrement reste en base de données
        /// pour conserver l'historique des tâches et projets associés.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à désactiver.</param>
        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // Soft delete - désactive l'utilisateur au lieu de le supprimer physiquement
                string query = "UPDATE utilisateurs SET est_actif = false WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Vérifie les identifiants de l'utilisateur.
        /// La comparaison se fait directement en base avec SHA2 pour ne jamais
        /// exposer le mot de passe haché côté application.
        /// </summary>
        /// <param name="email">Email saisi par l'utilisateur.</param>
        /// <param name="motDePasse">Mot de passe en clair à vérifier.</param>
        /// <returns>L'utilisateur connecté ou null si les identifiants sont incorrects.</returns>
        public Utilisateur Authentifier(string email, string motDePasse)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // La comparaison du hash se fait côté MySQL : SHA2(motDePasse saisie) == hash stocké
                // et on vérifie que le compte est actif
                string query = @"SELECT id, nom, email, date_creation, est_actif, role 
                               FROM utilisateurs 
                               WHERE email = @email AND mot_de_passe = SHA2(@motDePasse, 256) AND est_actif = true";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Authentification réussie : retourne l'objet utilisateur (sans mot de passe)
                            return new Utilisateur
                            {
                                Id = reader.GetInt32("id"),
                                Nom = reader.GetString("nom"),
                                Email = reader.GetString("email"),
                                DateCreation = reader.GetDateTime("date_creation"),
                                EstActif = reader.GetBoolean("est_actif"),
                                Role = reader.IsDBNull(reader.GetOrdinal("role")) ? "User" : reader.GetString("role")
                            };
                        }
                    }
                }
            }
            // Retourne null si l'authentification a échoué
            return null;
        }
    }
}