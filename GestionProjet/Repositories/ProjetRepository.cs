// =============================================================================
// Repository Projet - Implémentation concrète de IProjetRepository
// Effectue toutes les opérations SQL CRUD sur la table "Projets" et la table
// de liaison "Membres_Projet". La progression est calculée dynamiquement via
// une sous-requête SQL basée sur le ratio de tâches terminées.
// =============================================================================

using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Data;
using MySql.Data.MySqlClient;

namespace GestionProjet.Repositories
{
    /// <summary>
    /// Implémentation concrète du repository des projets.
    /// Gère la persistance des projets et de leurs membres via MySQL.
    /// La progression (%) est calculée en base via une sous-requête SQL.
    /// </summary>
    public class ProjetRepository : IProjetRepository
    {
        /// <summary>Contexte de base de données pour obtenir les connexions MySQL.</summary>
        private readonly DatabaseContext _context;

        /// <summary>
        /// Initialise le repository en créant une instance du contexte de base de données.
        /// </summary>
        public ProjetRepository()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retourne tous les projets de la base de données, triés par date de création décroissante.
        /// La progression est calculée via une sous-requête SQL (tâches terminées / total).
        /// </summary>
        /// <returns>Liste de tous les projets.</returns>
        public List<Projet> GetAll()
        {
            var projets = new List<Projet>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // La sous-requête calcule la progression = (nb tâches terminées / nb total) * 100
                // IFNULL(..., 0) retourne 0 si le projet n'a pas de tâches
                string query = "SELECT p.*, u.nom as createur_nom, " +
                               "(SELECT IFNULL(ROUND(SUM(CASE WHEN s.libelle = 'Terminé' THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 0) " +
                               " FROM Taches t JOIN Statuts s ON t.statut_id = s.id WHERE t.projet_id = p.id) as progression " +
                               "FROM Projets p LEFT JOIN utilisateurs u ON p.createur_id = u.id ORDER BY p.date_creation DESC";
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

        /// <summary>
        /// Retourne les projets dont l'utilisateur est membre via la table Membres_Projet.
        /// La progression est calculée via une sous-requête SQL.
        /// </summary>
        /// <param name="utilisateurId">Identifiant de l'utilisateur dont on recherche les projets.</param>
        /// <returns>Liste des projets auxquels l'utilisateur participe.</returns>
        public List<Projet> GetByMembre(int utilisateurId)
        {
            var projets = new List<Projet>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // JOIN sur Membres_Projet pour filtrer uniquement les projets de cet utilisateur
                string query = "SELECT p.*, u.nom as createur_nom, " +
                               "(SELECT IFNULL(ROUND(SUM(CASE WHEN s.libelle = 'Terminé' THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 0) " +
                               " FROM Taches t JOIN Statuts s ON t.statut_id = s.id WHERE t.projet_id = p.id) as progression " +
                               "FROM Projets p " +
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

        /// <summary>
        /// Retourne un projet par son identifiant unique, avec sa progression calculée.
        /// </summary>
        /// <param name="id">Identifiant du projet.</param>
        /// <returns>Le projet trouvé ou null.</returns>
        public Projet GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                string query = "SELECT p.*, u.nom as createur_nom, " +
                               "(SELECT IFNULL(ROUND(SUM(CASE WHEN s.libelle = 'Terminé' THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 0) " +
                               " FROM Taches t JOIN Statuts s ON t.statut_id = s.id WHERE t.projet_id = p.id) as progression " +
                               "FROM Projets p LEFT JOIN utilisateurs u ON p.createur_id = u.id WHERE p.id = @id";
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

        /// <summary>
        /// Insère un nouveau projet en base de données.
        /// Après insertion, récupère l'ID généré et l'assigne au projet.
        /// Ajoute également le créateur comme membre admin du projet.
        /// </summary>
        /// <param name="projet">Le projet à créer (son Id sera renseigné après insertion).</param>
        public void Add(Projet projet)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // LAST_INSERT_ID() récupère l'identifiant auto-généré après l'insertion
                string query = "INSERT INTO Projets (nom, description, date_fin_prevue, createur_id) VALUES (@nom, @desc, @dateFin, @createurId); SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nom", projet.Nom);
                    cmd.Parameters.AddWithValue("@desc", projet.Description);
                    // DBNull.Value si DateFinPrevue est null (champ optionnel)
                    cmd.Parameters.AddWithValue("@dateFin", (object)projet.DateFinPrevue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@createurId", (object)projet.CreateurId ?? DBNull.Value);
                    
                    // ExecScalar récupère le résultat de SELECT LAST_INSERT_ID()
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    projet.Id = newId;

                    // Ajouter le créateur comme membre par défaut avec le rôle "Admin"
                    if (projet.CreateurId.HasValue)
                    {
                        AjouterMembre(newId, projet.CreateurId.Value, "Admin");
                    }
                }
            }
        }

        /// <summary>
        /// Met à jour le nom, la description et la date de fin prévue d'un projet existant.
        /// </summary>
        /// <param name="projet">Le projet avec les nouvelles valeurs.</param>
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

        /// <summary>
        /// Supprime définitivement un projet de la base de données.
        /// Les tâches et membres associés sont supprimés en cascade (défini en base).
        /// </summary>
        /// <param name="id">Identifiant du projet à supprimer.</param>
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

        /// <summary>
        /// Ajoute un utilisateur comme membre d'un projet avec un rôle donné.
        /// "INSERT IGNORE" évite les doublons si l'utilisateur est déjà membre.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        /// <param name="utilisateurId">Identifiant de l'utilisateur à ajouter.</param>
        /// <param name="role">Rôle dans le projet (ex : "Admin", "Membre").</param>
        public void AjouterMembre(int projetId, int utilisateurId, string role)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // INSERT IGNORE : si la combinaison (projet_id, utilisateur_id) existe déjà,
                // la requête est ignorée sans déclencher d'erreur
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

        /// <summary>
        /// Retire un utilisateur de la liste des membres d'un projet.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        /// <param name="utilisateurId">Identifiant de l'utilisateur à retirer.</param>
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

        /// <summary>
        /// Retourne la liste des utilisateurs membres d'un projet donné.
        /// Utilisé pour peupler la liste d'assignation dans les formulaires de tâche.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        /// <returns>Liste des membres du projet.</returns>
        public List<Utilisateur> GetMembres(int projetId)
        {
            var membres = new List<Utilisateur>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                // JOIN via la table de liaison Membres_Projet
                string query = "SELECT u.* FROM utilisateurs u " +
                               "JOIN Membres_Projet mp ON u.id = mp.utilisateur_id " +
                               "WHERE mp.projet_id = @pId";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pId", projetId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            membres.Add(new Utilisateur
                            {
                                Id = reader.GetInt32("id"),
                                Nom = reader.GetString("nom"),
                                Email = reader.GetString("email")
                            });
                        }
                    }
                }
            }
            return membres;
        }

        /// <summary>
        /// Méthode privée qui mappe une ligne du lecteur MySQL en objet <see cref="Projet"/>.
        /// Centralise la logique de mapping pour éviter la duplication dans les méthodes Get.
        /// </summary>
        /// <param name="reader">Lecteur MySQL positionné sur une ligne valide.</param>
        /// <returns>Un objet <see cref="Projet"/> rempli avec les données de la ligne.</returns>
        private Projet MapReaderToProjet(MySqlDataReader reader)
        {
            return new Projet
            {
                Id = reader.GetInt32("id"),
                Nom = reader.GetString("nom"),
                // Description peut être NULL en base → chaîne vide par défaut
                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                DateCreation = reader.GetDateTime("date_creation"),
                // DateFinPrevue est optionnelle
                DateFinPrevue = reader.IsDBNull(reader.GetOrdinal("date_fin_prevue")) ? (DateTime?)null : reader.GetDateTime("date_fin_prevue"),
                Progression = Convert.ToInt32(reader["progression"]),
                CreateurId = reader.IsDBNull(reader.GetOrdinal("createur_id")) ? (int?)null : reader.GetInt32("createur_id"),
                // Création de l'objet Createur minimal (id + nom) depuis les colonnes jointes
                Createur = reader.IsDBNull(reader.GetOrdinal("createur_id")) ? null : new Utilisateur { Id = reader.GetInt32("createur_id"), Nom = reader.GetString("createur_nom") }
            };
        }
    }
}
