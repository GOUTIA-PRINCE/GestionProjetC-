using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Data;
using MySql.Data.MySqlClient;

namespace GestionProjet.Repositories
{
    public class TacheRepository : ITacheRepository
    {
        private readonly DatabaseContext _context;

        public TacheRepository()
        {
            _context = new DatabaseContext();
        }

        public List<Tache> GetAllByProjet(int projetId)
        {
            var taches = new List<Tache>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT t.*, s.libelle as statut_lib, p.libelle as priorite_lib, p.couleur_hex, u.nom as assignee_nom " +
                               "FROM Taches t " +
                               "JOIN Statuts s ON t.statut_id = s.id " +
                               "LEFT JOIN Priorites p ON t.priorite_id = p.id " +
                               "LEFT JOIN utilisateurs u ON t.assignee_id = u.id " +
                               "WHERE t.projet_id = @projetId ORDER BY s.ordre, t.date_creation DESC";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@projetId", projetId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taches.Add(MapReaderToTache(reader));
                        }
                    }
                }
            }
            return taches;
        }

        public List<Tache> GetAllByAssignee(int assigneeId)
        {
            var taches = new List<Tache>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT t.*, s.libelle as statut_lib, p.libelle as priorite_lib, p.couleur_hex, u.nom as assignee_nom " +
                               "FROM Taches t " +
                               "JOIN Statuts s ON t.statut_id = s.id " +
                               "LEFT JOIN Priorites p ON t.priorite_id = p.id " +
                               "LEFT JOIN utilisateurs u ON t.assignee_id = u.id " +
                               "WHERE t.assignee_id = @assigneeId";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@assigneeId", assigneeId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taches.Add(MapReaderToTache(reader));
                        }
                    }
                }
            }
            return taches;
        }

        public Tache GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT t.*, s.libelle as statut_lib, p.libelle as priorite_lib, p.couleur_hex, u.nom as assignee_nom " +
                               "FROM Taches t " +
                               "JOIN Statuts s ON t.statut_id = s.id " +
                               "LEFT JOIN Priorites p ON t.priorite_id = p.id " +
                               "LEFT JOIN utilisateurs u ON t.assignee_id = u.id " +
                               "WHERE t.id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapReaderToTache(reader);
                    }
                }
            }
            return null;
        }

        public void Add(Tache tache)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Taches (titre, description, date_echeance, projet_id, statut_id, priorite_id, assignee_id) " +
                               "VALUES (@titre, @description, @dateEcheance, @projetId, @statutId, @prioriteId, @assigneeId)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@titre", tache.Titre);
                    cmd.Parameters.AddWithValue("@description", tache.Description);
                    cmd.Parameters.AddWithValue("@dateEcheance", (object)tache.DateEcheance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@projetId", tache.ProjetId);
                    cmd.Parameters.AddWithValue("@statutId", tache.StatutId);
                    cmd.Parameters.AddWithValue("@prioriteId", (object)tache.PrioriteId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@assigneeId", (object)tache.AssigneeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Tache tache)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Taches SET titre=@titre, description=@description, date_echeance=@dateEcheance, " +
                               "statut_id=@statutId, priorite_id=@prioriteId, assignee_id=@assigneeId WHERE id=@id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", tache.Id);
                    cmd.Parameters.AddWithValue("@titre", tache.Titre);
                    cmd.Parameters.AddWithValue("@description", tache.Description);
                    cmd.Parameters.AddWithValue("@dateEcheance", (object)tache.DateEcheance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@statutId", tache.StatutId);
                    cmd.Parameters.AddWithValue("@prioriteId", (object)tache.PrioriteId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@assigneeId", (object)tache.AssigneeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Taches WHERE id=@id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStatut(int tacheId, int nouveauStatutId)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Taches SET statut_id=@statutId WHERE id=@id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", tacheId);
                    cmd.Parameters.AddWithValue("@statutId", nouveauStatutId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Statut> GetStatuts()
        {
            var statuts = new List<Statut>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Statuts ORDER BY ordre";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statuts.Add(new Statut { 
                            Id = reader.GetInt32("id"), 
                            Libelle = reader.GetString("libelle"),
                            Ordre = reader.GetInt32("ordre")
                        });
                    }
                }
            }
            return statuts;
        }

        public List<Priorite> GetPriorites()
        {
            var priorites = new List<Priorite>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Priorites";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        priorites.Add(new Priorite { 
                            Id = reader.GetInt32("id"), 
                            Libelle = reader.GetString("libelle"),
                            CouleurHex = reader.GetString("couleur_hex")
                        });
                    }
                }
            }
            return priorites;
        }

        private Tache MapReaderToTache(MySqlDataReader reader)
        {
            return new Tache
            {
                Id = reader.GetInt32("id"),
                Titre = reader.GetString("titre"),
                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                DateCreation = reader.GetDateTime("date_creation"),
                DateEcheance = reader.IsDBNull(reader.GetOrdinal("date_echeance")) ? (DateTime?)null : reader.GetDateTime("date_echeance"),
                ProjetId = reader.GetInt32("projet_id"),
                StatutId = reader.GetInt32("statut_id"),
                PrioriteId = reader.IsDBNull(reader.GetOrdinal("priorite_id")) ? (int?)null : reader.GetInt32("priorite_id"),
                AssigneeId = reader.IsDBNull(reader.GetOrdinal("assignee_id")) ? (int?)null : reader.GetInt32("assignee_id"),
                Statut = new Statut { Id = reader.GetInt32("statut_id"), Libelle = reader.GetString("statut_lib") },
                Priorite = reader.IsDBNull(reader.GetOrdinal("priorite_id")) ? null : new Priorite { Id = reader.GetInt32("priorite_id"), Libelle = reader.GetString("priorite_lib"), CouleurHex = reader.GetString("couleur_hex") },
                Assignee = reader.IsDBNull(reader.GetOrdinal("assignee_id")) ? null : new Utilisateur { Id = reader.GetInt32("assignee_id"), Nom = reader.GetString("assignee_nom") }
            };
        }
    }
}
