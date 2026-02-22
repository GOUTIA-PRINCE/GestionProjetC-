using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Data;
using MySql.Data.MySqlClient;

namespace GestionProjet.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly DatabaseContext _context;

        public UtilisateurRepository()
        {
            _context = new DatabaseContext();
        }

        public List<Utilisateur> GetAll()
        {
            var utilisateurs = new List<Utilisateur>();

            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, nom, email, date_creation, est_actif FROM utilisateurs WHERE est_actif = true ORDER BY nom";

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
                            EstActif = reader.GetBoolean("est_actif")
                        });
                    }
                }
            }

            return utilisateurs;
        }

        public Utilisateur GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, nom, email, date_creation, est_actif FROM utilisateurs WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
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
                                EstActif = reader.GetBoolean("est_actif")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public Utilisateur GetByEmail(string email)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT id, nom, email, mot_de_passe, date_creation, est_actif FROM utilisateurs WHERE email = @email";

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
                                EstActif = reader.GetBoolean("est_actif")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Utilisateur utilisateur)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO utilisateurs (nom, email, mot_de_passe, date_creation, est_actif) 
                               VALUES (@nom, @email, SHA2(@motDePasse, 256), @dateCreation, @estActif)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", utilisateur.Nom);
                    cmd.Parameters.AddWithValue("@email", utilisateur.Email);
                    cmd.Parameters.AddWithValue("@motDePasse", utilisateur.MotDePasse);
                    cmd.Parameters.AddWithValue("@dateCreation", DateTime.Now);
                    cmd.Parameters.AddWithValue("@estActif", true);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Utilisateur utilisateur)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE utilisateurs 
                               SET nom = @nom, email = @email, est_actif = @estActif 
                               WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@nom", utilisateur.Nom);
                    cmd.Parameters.AddWithValue("@email", utilisateur.Email);
                    cmd.Parameters.AddWithValue("@estActif", utilisateur.EstActif);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // Soft delete - désactive l'utilisateur au lieu de le supprimer
                string query = "UPDATE utilisateurs SET est_actif = false WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Utilisateur Authentifier(string email, string motDePasse)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = @"SELECT id, nom, email, date_creation, est_actif 
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
                            return new Utilisateur
                            {
                                Id = reader.GetInt32("id"),
                                Nom = reader.GetString("nom"),
                                Email = reader.GetString("email"),
                                DateCreation = reader.GetDateTime("date_creation"),
                                EstActif = reader.GetBoolean("est_actif")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}