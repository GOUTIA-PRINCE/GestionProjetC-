// =============================================================================
// Contrôleur de connexion (LoginController)
// Gère la logique métier du formulaire de login :
//   - Authentification de l'utilisateur
//   - Navigation vers le formulaire principal après connexion
//   - Navigation vers le formulaire d'inscription
//   - Quitter l'application
// =============================================================================

using GestionProjet.Repositories;
using GestionProjet.Models;
using GestionProjet.Views;
using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur gérant les actions du formulaire de connexion (LoginForm).
    /// Hérite de <see cref="BaseController"/> pour la navigation et les messages.
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>Référence vers le formulaire de connexion (vue).</summary>
        private readonly LoginForm _loginForm;

        /// <summary>Repository pour accéder aux données des utilisateurs.</summary>
        private readonly IUtilisateurRepository _utilisateurRepository;

        /// <summary>
        /// Initialise le contrôleur de login en s'enregistrant auprès du formulaire.
        /// </summary>
        /// <param name="loginForm">Le formulaire de connexion à contrôler.</param>
        public LoginController(LoginForm loginForm)
        {
            _loginForm = loginForm;
            _utilisateurRepository = new UtilisateurRepository();
            // Enregistre ce contrôleur auprès de la vue
            _loginForm.SetController(this);
            // Définit le formulaire courant pour la navigation
            CurrentForm = loginForm;
        }

        /// <summary>
        /// Tente d'authentifier l'utilisateur avec les identifiants fournis.
        /// En cas de succès, ouvre le formulaire principal.
        /// En cas d'échec, affiche un message d'erreur.
        /// </summary>
        /// <param name="email">Email saisi dans le formulaire.</param>
        /// <param name="motDePasse">Mot de passe saisi dans le formulaire.</param>
        public void Authentifier(string email, string motDePasse)
        {
            // Validation : les deux champs doivent être remplis
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(motDePasse))
            {
                AfficherErreur("Veuillez remplir tous les champs", "Erreur de saisie");
                return;
            }

            // Tentative d'authentification via le repository (vérification SHA-256 en base)
            var utilisateur = _utilisateurRepository.Authentifier(email, motDePasse);

            if (utilisateur != null)
            {
                // Connexion réussie : message de bienvenue
                AfficherMessage($"Bienvenue {utilisateur.Nom} !", "Connexion réussie");

                // Ouvrir le formulaire principal et créer son contrôleur
                var mainForm = new MainForm();
                var mainController = new MainController(mainForm, utilisateur);
                NaviguerVers(mainForm);
            }
            else
            {
                // Identifiants incorrects ou compte inactif
                AfficherErreur("Email ou mot de passe incorrect", "Échec de la connexion");
            }
        }

        /// <summary>
        /// Demande confirmation à l'utilisateur puis quitte l'application.
        /// </summary>
        public void QuitterApplication()
        {
            if (ConfirmerAction("Voulez-vous vraiment quitter l'application ?"))
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Navigue vers le formulaire d'inscription (RegisterForm).
        /// Crée le contrôleur d'inscription en même temps que la vue.
        /// </summary>
        public void AfficherPageInscription()
        {
            var registerForm = new RegisterForm();
            var registerController = new RegisterController(registerForm);
            NaviguerVers(registerForm);
        }

        /// <summary>
        /// Affiche un message indiquant que la fonctionnalité de mot de passe oublié
        /// n'est pas encore implémentée.
        /// </summary>
        public void AfficherPageMotDePasseOublie()
        {
            AfficherMessage("Cette fonctionnalité sera bientôt disponible.", "Information");
        }
    }
}