using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Data;
using MySql.Data.MySqlClient;

namespace GestionProjet.Repositories
{
    public class ProjetRepository : IProjetRepository
    {
        private readonly DatabaseContext _context;

        public ProjetRepository()
        {
            _context = new DatabaseContext();
        }

        public List<Projet> GetAll()
        {
            var projets = new List<Projet>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT p.*, u.nom as createur_nom FROM Projets p LEFT JOIN utilisateurs u ON p.createur_id = u.id ORDER BY p.date_creation DESC";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        projets.Add(MapReaderToProjet(reader));
                    }
                }
            }
            return projets;
        }

        public List<Projet> GetByMembre(int utilisateurId)
        {
            var projets = new List<Projet>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT p.*, u.nom as createur_nom FROM Projets p " +
                               "JOIN Membres_Projet mp ON p.id = mp.projet_id " +
                               "LEFT JOIN utilisateurs u ON p.createur_id = u.id " +
                               "WHERE mp.utilisateur_id = @userId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", utilisateurId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projets.Add(MapReaderToProjet(reader));
                        }
                    }
                }
            }
            return projets;
        }

        public Projet GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT p.*, u.nom as createur_nom FROM Projets p LEFT JOIN utilisateurs u ON p.createur_id = u.id WHERE p.id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapReaderToProjet(reader);
                    }
                }
            }
            return null;
        }

        public void Add(Projet projet)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Projets (nom, description, date_fin_prevue, createur_id) VALUES (@nom, @desc, @dateFin, @createurId); SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", projet.Nom);
                    cmd.Parameters.AddWithValue("@desc", projet.Description);
                    cmd.Parameters.AddWithValue("@dateFin", (object)projet.DateFinPrevue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@createurId", (object)projet.CreateurId ?? DBNull.Value);
                    
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    projet.Id = newId;

                    // Ajouter le créateur comme membre par défaut
                    if (projet.CreateurId.HasValue)
                    {
                        AjouterMembre(newId, projet.CreateurId.Value, "Admin");
                    }
                }
            }
        }

        public void Update(Projet projet)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Projets SET nom=@nom, description=@desc, date_fin_prevue=@dateFin WHERE id=@id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", projet.Id);
                    cmd.Parameters.AddWithValue("@nom", projet.Nom);
                    cmd.Parameters.AddWithValue("@desc", projet.Description);
                    cmd.Parameters.AddWithValue("@dateFin", (object)projet.DateFinPrevue ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Projets WHERE id=@id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AjouterMembre(int projetId, int utilisateurId, string role)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "INSERT IGNORE INTO Membres_Projet (projet_id, utilisateur_id, role) VALUES (@pId, @uId, @role)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pId", projetId);
                    cmd.Parameters.AddWithValue("@uId", utilisateurId);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RetirerMembre(int projetId, int utilisateurId)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Membres_Projet WHERE projet_id=@pId AND utilisateur_id=@uId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pId", projetId);
                    cmd.Parameters.AddWithValue("@uId", utilisateurId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private Projet MapReaderToProjet(MySqlDataReader reader)
        {
            return new Projet
            {
                Id = reader.GetInt32("id"),
                Nom = reader.GetString("nom"),
                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                DateCreation = reader.GetDateTime("date_creation"),
                DateFinPrevue = reader.IsDBNull(reader.GetOrdinal("date_fin_prevue")) ? (DateTime?)null : reader.GetDateTime("date_fin_prevue"),
                CreateurId = reader.IsDBNull(reader.GetOrdinal("createur_id")) ? (int?)null : reader.GetInt32("createur_id"),
                Createur = reader.IsDBNull(reader.GetOrdinal("createur_id")) ? null : new Utilisateur { Id = reader.GetInt32("createur_id"), Nom = reader.GetString("createur_nom") }
            };
        }
    }
}
