// =============================================================================
// Interface IUtilisateurRepository
// Contrat définissant toutes les opérations CRUD et d'authentification
// applicables aux utilisateurs. Les classes concrètes (ex: UtilisateurRepository)
// doivent implémenter cette interface pour garantir la cohérence.
// =============================================================================

using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Repositories
{
    /// <summary>
    /// Interface définissant les opérations de persistance pour les utilisateurs.
    /// Respecte le principe de ségrégation des interfaces (ISP) du SOLID.
    /// </summary>
    public interface IUtilisateurRepository
    {
        // ── Lecture ───────────────────────────────────────────────────────────

        /// <summary>Retourne la liste de tous les utilisateurs actifs.</summary>
        List<Utilisateur> GetAll();

        /// <summary>
        /// Recherche un utilisateur par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant unique de l'utilisateur.</param>
        /// <returns>L'utilisateur trouvé ou null.</returns>
        Utilisateur GetById(int id);

        /// <summary>
        /// Recherche un utilisateur par son adresse email.
        /// </summary>
        /// <param name="email">Email de l'utilisateur.</param>
        /// <returns>L'utilisateur trouvé ou null.</returns>
        Utilisateur GetByEmail(string email);

        // ── Création ──────────────────────────────────────────────────────────

        /// <summary>
        /// Ajoute un nouvel utilisateur en base de données.
        /// Le mot de passe est haché (SHA-256) avant la sauvegarde.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à ajouter.</param>
        void Add(Utilisateur utilisateur);

        // ── Mise à jour ───────────────────────────────────────────────────────

        /// <summary>
        /// Met à jour les informations d'un utilisateur existant.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur avec les nouvelles valeurs.</param>
        void Update(Utilisateur utilisateur);

        // ── Suppression ───────────────────────────────────────────────────────

        /// <summary>
        /// Effectue un "soft delete" de l'utilisateur : désactive son compte
        /// (est_actif = false) sans supprimer l'enregistrement en base.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à désactiver.</param>
        void Delete(int id);

        // ── Authentification ──────────────────────────────────────────────────

        /// <summary>
        /// Vérifie les identifiants de l'utilisateur en base de données.
        /// </summary>
        /// <param name="email">Email de l'utilisateur.</param>
        /// <param name="motDePasse">Mot de passe en clair (comparé au hash SHA-256 en base).</param>
        /// <returns>L'utilisateur authentifié ou null si les identifiants sont invalides.</returns>
        Utilisateur Authentifier(string email, string motDePasse);
    }
}