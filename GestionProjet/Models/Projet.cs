// =============================================================================
// Modèle Projet
// Représente un projet de gestion. Un projet contient un ensemble de tâches,
// est créé par un utilisateur (créateur), et possède une progression calculée
// dynamiquement à partir des tâches terminées.
// =============================================================================

using System;
using System.Collections.Generic;

namespace GestionProjet.Models
{
    /// <summary>
    /// Modèle représentant un projet dans l'application.
    /// Un projet regroupe des tâches et des membres qui y collaborent.
    /// </summary>
    public class Projet
    {
        /// <summary>Identifiant unique du projet (clé primaire en base).</summary>
        public int Id { get; set; }

        /// <summary>Nom du projet.</summary>
        public string Nom { get; set; }

        /// <summary>Description détaillée du projet.</summary>
        public string Description { get; set; }

        /// <summary>Date et heure de création du projet.</summary>
        public DateTime DateCreation { get; set; }

        /// <summary>Date de fin prévue du projet (facultative).</summary>
        public DateTime? DateFinPrevue { get; set; }

        /// <summary>
        /// Identifiant de l'utilisateur qui a créé le projet.
        /// Nullable car le créateur pourrait être supprimé (soft delete).
        /// </summary>
        public int? CreateurId { get; set; }

        /// <summary>
        /// Pourcentage d'avancement du projet (0 à 100).
        /// Calculé en base de données : (tâches terminées / total tâches) × 100.
        /// </summary>
        public int Progression { get; set; }

        // ── Propriétés de navigation ──────────────────────────────────────────

        /// <summary>
        /// Objet utilisateur représentant le créateur du projet (chargé en JOIN).
        /// </summary>
        public Utilisateur Createur { get; set; }

        /// <summary>
        /// Liste des tâches associées à ce projet (chargée séparément si nécessaire).
        /// </summary>
        public List<Tache> Taches { get; set; }

        /// <summary>
        /// Constructeur par défaut.
        /// Initialise la date de création, la liste de tâches vide et la progression à 0.
        /// </summary>
        public Projet()
        {
            DateCreation = DateTime.Now;
            Taches = new List<Tache>();
            Progression = 0;
        }
    }
}
