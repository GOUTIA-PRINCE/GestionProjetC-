// =============================================================================
// Modèle Tache
// Représente une tâche appartenant à un projet. Une tâche possède un titre,
// une description, une date d'échéance, un statut (À faire / En cours / Terminé),
// une priorité et un utilisateur assigné.
// =============================================================================

using System;

namespace GestionProjet.Models
{
    /// <summary>
    /// Modèle représentant une tâche dans un projet.
    /// Une tâche est associée à un projet, possède un statut et peut être
    /// assignée à un membre du projet.
    /// </summary>
    public class Tache
    {
        /// <summary>Identifiant unique de la tâche (clé primaire en base).</summary>
        public int Id { get; set; }

        /// <summary>Titre court de la tâche.</summary>
        public string Titre { get; set; }

        /// <summary>Description détaillée de la tâche (facultatif).</summary>
        public string Description { get; set; }

        /// <summary>Date et heure de création de la tâche.</summary>
        public DateTime DateCreation { get; set; }

        /// <summary>Date limite d'exécution de la tâche (facultative).</summary>
        public DateTime? DateEcheance { get; set; }

        /// <summary>Identifiant du projet auquel appartient cette tâche.</summary>
        public int ProjetId { get; set; }

        /// <summary>Identifiant du statut actuel de la tâche (ex : "À faire", "En cours", "Terminé").</summary>
        public int StatutId { get; set; }

        /// <summary>Identifiant de la priorité de la tâche (Basse, Normale, Haute, Urgente). Nullable.</summary>
        public int? PrioriteId { get; set; }

        /// <summary>Identifiant de l'utilisateur assigné à cette tâche. Nullable si non assignée.</summary>
        public int? AssigneeId { get; set; }

        // ── Propriétés de navigation ──────────────────────────────────────────

        /// <summary>Projet auquel appartient cette tâche (chargé via JOIN).</summary>
        public Projet Projet { get; set; }

        /// <summary>Statut actuel de la tâche (chargé via JOIN).</summary>
        public Statut Statut { get; set; }

        /// <summary>Priorité de la tâche (chargée via JOIN).</summary>
        public Priorite Priorite { get; set; }

        /// <summary>Utilisateur assigné à la tâche (chargé via JOIN).</summary>
        public Utilisateur Assignee { get; set; }

        /// <summary>
        /// Constructeur par défaut.
        /// Initialise la date de création à l'instant présent.
        /// </summary>
        public Tache()
        {
            DateCreation = DateTime.Now;
        }
    }
}
