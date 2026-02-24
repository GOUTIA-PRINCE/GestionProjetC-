// =============================================================================
// Repository Tache - Implémentation concrète de ITacheRepository
// Effectue toutes les opérations SQL CRUD sur la table "Taches".
// Récupère aussi les référentiels Statuts et Priorites utilisés dans les
// formulaires et le tableau Kanban.
// =============================================================================

using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Data;
using MySql.Data.MySqlClient;

namespace GestionProjet.Repositories
{
    /// <summary>
    /// Implémentation concrète du repository des tâches.
    /// Gère la persistance des tâches et les jointures avec Statuts, Priorites et Utilisateurs.
    /// </summary>
    public class TacheRepository : ITacheRepository
    {
        /// <summary>Contexte de base de données pour obtenir les connexions MySQL.</summary>
        private readonly DatabaseContext _context;

        /// <summary>
        /// Initialise le repository en créant une instance du contexte de base de données.
        /// </summary>
        public TacheRepository()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retourne toutes les tâches d'un projet donné, triées par statut (ordre Kanban)
        /// puis par date de création décroissante.
        /// Inclut les informations de statut, priorité et assignée via des JOIN.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        /// <returns>Liste des tâches du projet.</returns>
        public List<Tache> GetAllByProjet(int projetId)
        {
            var taches = new List<Tache>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // JOIN sur Statuts (obligatoire), Priorites (optionnel), Utilisateurs (optionnel)
                // Les priorités et assignés peuvent être NULL → LEFT JOIN
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

        /// <summary>
        /// Retourne toutes les tâches assignées à un utilisateur particulier.
        /// Utilisé pour afficher "Mes tâches" dans le dashboard.
        /// </summary>
        /// <param name="assigneeId">Identifiant de l'utilisateur assigné.</param>
        /// <returns>Liste des tâches assignées à l'utilisateur.</returns>
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

        /// <summary>
        /// Retourne une tâche par son identifiant unique, avec toutes ses relations chargées.
        /// </summary>
        /// <param name="id">Identifiant de la tâche.</param>
        /// <returns>La tâche trouvée ou null.</returns>
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

        /// <summary>
        /// Insère une nouvelle tâche en base de données.
        /// Les champs date_echeance, priorite_id et assignee_id sont optionnels (peuvent être NULL).
        /// </summary>
        /// <param name="tache">La tâche à insérer.</param>
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
                    // DBNull.Value si la date ou l'id optionnel n'est pas renseigné
                    cmd.Parameters.AddWithValue("@dateEcheance", (object)tache.DateEcheance ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@projetId", tache.ProjetId);
                    cmd.Parameters.AddWithValue("@statutId", tache.StatutId);
                    cmd.Parameters.AddWithValue("@prioriteId", (object)tache.PrioriteId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@assigneeId", (object)tache.AssigneeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Met à jour les informations d'une tâche existante.
        /// Les champs date_echeance, priorite_id et assignee_id peuvent être mis à NULL.
        /// </summary>
        /// <param name="tache">La tâche avec les nouvelles valeurs.</param>
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

        /// <summary>
        /// Supprime définitivement une tâche de la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la tâche à supprimer.</param>
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

        /// <summary>
        /// Met à jour uniquement le statut d'une tâche.
        /// Appelé lors du drag-and-drop dans le tableau Kanban pour changer
        /// rapidement de colonne sans ouvrir le formulaire complet.
        /// </summary>
        /// <param name="tacheId">Identifiant de la tâche à mettre à jour.</param>
        /// <param name="nouveauStatutId">Identifiant du nouveau statut.</param>
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

        /// <summary>
        /// Retourne la liste de tous les statuts disponibles, triés par ordre d'affichage.
        /// Utilisé pour créer les colonnes du Kanban et peupler les ComboBox.
        /// </summary>
        /// <returns>Liste ordonnée des statuts.</returns>
        public List<Statut> GetStatuts()
        {
            var statuts = new List<Statut>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // ORDER BY ordre pour respecter l'ordre des colonnes Kanban
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

        /// <summary>
        /// Retourne la liste de toutes les priorités disponibles.
        /// Utilisé pour peupler le ComboBox de priorité dans le formulaire de tâche.
        /// </summary>
        /// <returns>Liste des priorités avec leurs couleurs hexadécimales.</returns>
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

        /// <summary>
        /// Méthode privée qui mappe une ligne du lecteur MySQL en objet <see cref="Tache"/>.
        /// Centralise la logique de mapping pour éviter la duplication dans les méthodes Get.
        /// Gère les valeurs NULL pour les champs optionnels (date_echeance, priorite, assignee).
        /// </summary>
        /// <param name="reader">Lecteur MySQL positionné sur une ligne valide.</param>
        /// <returns>Un objet <see cref="Tache"/> rempli avec toutes ses relations.</returns>
        private Tache MapReaderToTache(MySqlDataReader reader)
        {
            return new Tache
            {
                Id = reader.GetInt32("id"),
                Titre = reader.GetString("titre"),
                // Description peut être NULL en base → chaîne vide par défaut
                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                DateCreation = reader.GetDateTime("date_creation"),
                // Date d'échéance optionnelle
                DateEcheance = reader.IsDBNull(reader.GetOrdinal("date_echeance")) ? (DateTime?)null : reader.GetDateTime("date_echeance"),
                ProjetId = reader.GetInt32("projet_id"),
                StatutId = reader.GetInt32("statut_id"),
                // Priorité optionnelle
                PrioriteId = reader.IsDBNull(reader.GetOrdinal("priorite_id")) ? (int?)null : reader.GetInt32("priorite_id"),
                // Assigné optionnel
                AssigneeId = reader.IsDBNull(reader.GetOrdinal("assignee_id")) ? (int?)null : reader.GetInt32("assignee_id"),
                // Objet Statut minimal chargé via JOIN (toujours présent)
                Statut = new Statut { Id = reader.GetInt32("statut_id"), Libelle = reader.GetString("statut_lib") },
                // Objet Priorite minimal chargé via LEFT JOIN (peut être null)
                Priorite = reader.IsDBNull(reader.GetOrdinal("priorite_id")) ? null : new Priorite { Id = reader.GetInt32("priorite_id"), Libelle = reader.GetString("priorite_lib"), CouleurHex = reader.GetString("couleur_hex") },
                // Objet Assignee minimal chargé via LEFT JOIN (peut être null)
                Assignee = reader.IsDBNull(reader.GetOrdinal("assignee_id")) ? null : new Utilisateur { Id = reader.GetInt32("assignee_id"), Nom = reader.GetString("assignee_nom") }
            };
        }
    }
}
