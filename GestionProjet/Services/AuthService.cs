// =============================================================================
// Service d'authentification
// Gère la session utilisateur côté application : connexion, déconnexion
// et accès à l'utilisateur actuellement connecté.
// C'est la couche service qui fait l'intermédiaire entre le controller
// et le repository utilisateur pour l'authentification.
// =============================================================================

using GestionProjet.Models;
using GestionProjet.Repositories;

namespace GestionProjet.Services
{
    /// <summary>
    /// Service responsable de la gestion de l'authentification et de la session
    /// de l'utilisateur courant.
    /// </summary>
    public class AuthService
    {
        /// <summary>Repository d'accès aux données utilisateur.</summary>
        private readonly IUtilisateurRepository _utilisateurRepository;

        /// <summary>Utilisateur actuellement connecté, ou null si personne n'est connecté.</summary>
        private Utilisateur _utilisateurCourant;

        /// <summary>
        /// Initialise le service d'authentification en créant une instance
        /// du repository utilisateur.
        /// </summary>
        public AuthService()
        {
            _utilisateurRepository = new UtilisateurRepository();
        }

        /// <summary>
        /// Retourne l'utilisateur actuellement connecté, ou null s'il n'y en a pas.
        /// </summary>
        public Utilisateur UtilisateurCourant => _utilisateurCourant;

        /// <summary>
        /// Indique si un utilisateur est actuellement connecté à l'application.
        /// Retourne true si <see cref="UtilisateurCourant"/> n'est pas null.
        /// </summary>
        public bool EstConnecte => _utilisateurCourant != null;

        /// <summary>
        /// Tente d'authentifier un utilisateur avec son email et son mot de passe.
        /// En cas de succès, stocke l'utilisateur dans la session courante.
        /// </summary>
        /// <param name="email">Email saisi par l'utilisateur.</param>
        /// <param name="motDePasse">Mot de passe en clair saisi par l'utilisateur.</param>
        /// <returns>true si l'authentification a réussi, false sinon.</returns>
        public bool Connexion(string email, string motDePasse)
        {
            // Vérification des identifiants via le repository (comparaison SHA-256 en base)
            _utilisateurCourant = _utilisateurRepository.Authentifier(email, motDePasse);
            return EstConnecte;
        }

        /// <summary>
        /// Déconnecte l'utilisateur courant en réinitialisant la session à null.
        /// </summary>
        public void Deconnexion()
        {
            _utilisateurCourant = null;
        }
    }
}