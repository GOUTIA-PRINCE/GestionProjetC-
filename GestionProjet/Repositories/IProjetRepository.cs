// =============================================================================
// Interface IProjetRepository
// Contrat définissant toutes les opérations CRUD sur les projets,
// ainsi que la gestion des membres d'un projet.
// =============================================================================

using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Repositories
{
    /// <summary>
    /// Interface définissant les opérations de persistance pour les projets.
    /// Inclut la gestion des membres associés à chaque projet.
    /// </summary>
    public interface IProjetRepository
    {
        /// <summary>Retourne tous les projets de la base de données.</summary>
        List<Projet> GetAll();

        /// <summary>
        /// Retourne les projets dont l'utilisateur est membre.
        /// </summary>
        /// <param name="utilisateurId">Identifiant de l'utilisateur membre.</param>
        List<Projet> GetByMembre(int utilisateurId);

        /// <summary>
        /// Retourne un projet par son identifiant unique.
        /// </summary>
        /// <param name="id">Identifiant du projet.</param>
        /// <returns>Le projet trouvé ou null.</returns>
        Projet GetById(int id);

        /// <summary>
        /// Ajoute un nouveau projet en base de données.
        /// Après insertion, l'identifiant généré est assigné au projet.
        /// </summary>
        /// <param name="projet">Le projet à créer.</param>
        void Add(Projet projet);

        /// <summary>
        /// Met à jour les informations d'un projet existant.
        /// </summary>
        /// <param name="projet">Le projet avec les nouvelles valeurs.</param>
        void Update(Projet projet);

        /// <summary>
        /// Supprime un projet et toutes ses dépendances (tâches, membres).
        /// </summary>
        /// <param name="id">Identifiant du projet à supprimer.</param>
        void Delete(int id);

        /// <summary>
        /// Ajoute un utilisateur comme membre d'un projet avec un rôle donné.
        /// Utilise INSERT IGNORE pour éviter les doublons.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        /// <param name="utilisateurId">Identifiant de l'utilisateur à ajouter.</param>
        /// <param name="role">Rôle dans le projet (ex : "Admin", "Membre").</param>
        void AjouterMembre(int projetId, int utilisateurId, string role);

        /// <summary>
        /// Retire un utilisateur des membres d'un projet.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        /// <param name="utilisateurId">Identifiant de l'utilisateur à retirer.</param>
        void RetirerMembre(int projetId, int utilisateurId);

        /// <summary>
        /// Retourne la liste des membres (utilisateurs) d'un projet.
        /// </summary>
        /// <param name="projetId">Identifiant du projet.</param>
        List<Utilisateur> GetMembres(int projetId);
    }
}
