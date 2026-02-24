// =============================================================================
// Contrôleur d'inscription (RegisterController)
// Gère la logique métier du formulaire d'inscription :
//   - Validation et création d'un nouveau compte utilisateur
//   - Vérification de l'unicité de l'email
//   - Retour au formulaire de connexion après inscription
// =============================================================================

using System;
using GestionProjet.Repositories;
using GestionProjet.Models;
using GestionProjet.Views;
using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur gérant les actions du formulaire d'inscription (RegisterForm).
    /// Hérite de <see cref="BaseController"/> pour la navigation et les messages.
    /// </summary>
    public class RegisterController : BaseController
    {
        /// <summary>Référence vers le formulaire d'inscription (vue).</summary>
        private readonly RegisterForm _registerForm;

        /// <summary>Repository pour accéder et modifier les données des utilisateurs.</summary>
        private readonly IUtilisateurRepository _utilisateurRepository;

        /// <summary>
        /// Initialise le contrôleur d'inscription en s'enregistrant auprès du formulaire.
        /// </summary>
        /// <param name="registerForm">Le formulaire d'inscription à contrôler.</param>
        public RegisterController(RegisterForm registerForm)
        {
            _registerForm = registerForm;
            _utilisateurRepository = new UtilisateurRepository();
            // Enregistre ce contrôleur auprès de la vue
            _registerForm.SetController(this);
            // Définit le formulaire courant pour la navigation
            CurrentForm = registerForm;
        }

        /// <summary>
        /// Tente de créer un nouveau compte utilisateur.
        /// Vérifie que tous les champs sont remplis et que l'email n'est pas déjà utilisé.
        /// </summary>
        /// <param name="nom">Nom complet saisi.</param>
        /// <param name="email">Email saisi (doit être unique en base).</param>
        /// <param name="motDePasse">Mot de passe en clair (sera haché en base).</param>
        public void Inscrire(string nom, string email, string motDePasse)
        {
            // Validation : tous les champs sont obligatoires
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(motDePasse))
            {
                AfficherErreur("Veuillez remplir tous les champs", "Erreur de saisie");
                return;
            }

            try
            {
                // Vérification de l'unicité de l'email
                var existant = _utilisateurRepository.GetByEmail(email);
                if (existant != null)
                {
                    AfficherErreur("Cet email est déjà utilisé", "Erreur");
                    return;
                }

                // Création du nouvel utilisateur avec le rôle "User" par défaut
                var nouvelUtilisateur = new Utilisateur
                {
                    Nom = nom,
                    Email = email,
                    MotDePasse = motDePasse, // Sera haché par le repository (SHA-256)
                    EstActif = true,
                    DateCreation = DateTime.Now
                };

                // Persistance en base de données
                _utilisateurRepository.Add(nouvelUtilisateur);
                AfficherMessage("Compte créé avec succès ! Vous pouvez maintenant vous connecter.", "Succès");
                
                // Redirection vers le login après inscription réussie
                RetourLogin();
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la création du compte : {ex.Message}");
            }
        }

        /// <summary>
        /// Retourne au formulaire de connexion sans créer de compte.
        /// Crée un nouveau LoginForm et son contrôleur associé.
        /// </summary>
        public void RetourLogin()
        {
            var loginForm = new LoginForm();
            var loginController = new LoginController(loginForm);
            NaviguerVers(loginForm);
        }
    }
}
