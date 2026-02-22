using GestionProjet.Repositories;
using GestionProjet.Models;
using GestionProjet.Views;
using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LoginForm _loginForm;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public LoginController(LoginForm loginForm)
        {
            _loginForm = loginForm;
            _utilisateurRepository = new UtilisateurRepository();
            _loginForm.SetController(this);
            CurrentForm = loginForm;
        }

        public void Authentifier(string email, string motDePasse)
        {
            // Validation des champs
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(motDePasse))
            {
                AfficherErreur("Veuillez remplir tous les champs", "Erreur de saisie");
                return;
            }

            // Tentative d'authentification
            var utilisateur = _utilisateurRepository.Authentifier(email, motDePasse);

            if (utilisateur != null)
            {
                // Connexion réussie
                AfficherMessage($"Bienvenue {utilisateur.Nom} !", "Connexion réussie");

                // Ouvrir le formulaire principal
                var mainForm = new MainForm();
                var mainController = new MainController(mainForm, utilisateur);
                NaviguerVers(mainForm);
            }
            else
            {
                AfficherErreur("Email ou mot de passe incorrect", "Échec de la connexion");
            }
        }

        public void QuitterApplication()
        {
            if (ConfirmerAction("Voulez-vous vraiment quitter l'application ?"))
            {
                Application.Exit();
            }
        }
    }
}