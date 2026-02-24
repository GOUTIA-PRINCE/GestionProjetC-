// =============================================================================
// Interface ITacheRepository
// Contrat définissant toutes les opérations CRUD sur les tâches,
// le changement de statut rapide, ainsi que la récupération des référentiels
// (statuts et priorités disponibles).
// =============================================================================

using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Repositories
{
    /// <summary>
    /// Interface définissant les opérations de persistance pour les tâches.
    /// Permet également de récupérer les listes de référentiel (statuts et priorités).
    /// </summary>
    public interface ITacheRepository
    {
        /// <summary>
        /// Retourne toutes les tâches d'un projet donné, triées par statut puis par date.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        List<Tache> GetAllByProjet(int projetId);

        /// <summary>
        /// Retourne toutes les tâches assignées à un utilisateur donné.
        /// </summary>
        /// <param name="assigneeId">Identifiant de l'utilisateur assigné.</param>
        List<Tache> GetAllByAssignee(int assigneeId);

        /// <summary>
        /// Retourne une tâche par son identifiant unique.
        /// </summary>
        /// <param name="id">Identifiant de la tâche.</param>
        /// <returns>La tâche trouvée ou null.</returns>
        Tache GetById(int id);

        /// <summary>
        /// Ajoute une nouvelle tâche en base de données.
        /// </summary>
        /// <param name="tache">La tâche à créer.</param>
        void Add(Tache tache);

        /// <summary>
        /// Met à jour les informations d'une tâche existante.
        /// </summary>
        /// <param name="tache">La tâche avec les nouvelles valeurs.</param>
        void Update(Tache tache);

        /// <summary>
        /// Supprime définitivement une tâche de la base de données.
        /// </summary>
        /// <param name="id">Identifiant de la tâche à supprimer.</param>
        void Delete(int id);

        /// <summary>
        /// Modifie uniquement le statut d'une tâche (optimisé pour le drag-and-drop Kanban).
        /// </summary>
        /// <param name="tacheId">Identifiant de la tâche à mettre à jour.</param>
        /// <param name="nouveauStatutId">Identifiant du nouveau statut.</param>
        void UpdateStatut(int tacheId, int nouveauStatutId);

        /// <summary>
        /// Retourne la liste de tous les statuts disponibles, ordonnés par leur ordre d'affichage.
        /// Utilisé pour peupler les ComboBox et créer les colonnes du Kanban.
        /// </summary>
        List<Statut> GetStatuts();

        /// <summary>
        /// Retourne la liste de toutes les priorités disponibles.
        /// Utilisé pour peupler les ComboBox dans le formulaire de tâche.
        /// </summary>
        List<Priorite> GetPriorites();
    }
}
