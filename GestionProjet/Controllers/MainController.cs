
using GestionProjet.Models;
using GestionProjet.Views;
using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    public class MainController : BaseController
    {
        private readonly MainForm _mainForm;
        private readonly Utilisateur _utilisateurCourant;

        public MainController(MainForm mainForm, Utilisateur utilisateur)
        {
            _mainForm = mainForm;
            _utilisateurCourant = utilisateur;
            _mainForm.SetController(this, utilisateur);
            CurrentForm = mainForm;
        }

        public void OuvrirGestionUtilisateurs()
        {
            var utilisateurForm = new UtilisateurForm();
            var utilisateurController = new UtilisateurController(utilisateurForm);
            utilisateurForm.Show();
        }

        public void Deconnexion()
        {
            if (ConfirmerAction("Voulez-vous vraiment vous déconnecter ?"))
            {
                CurrentForm.Close();
                var loginForm = new LoginForm();
                var loginController = new LoginController(loginForm);
                loginForm.Show();
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