
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
            BaseController.MainView = mainForm;
            
            // Afficher le dashboard au démarrage
            AfficherDashboard();
        }

        public void AfficherDashboard()
        {
            var dashboard = new DashboardForm();
            // On pourrait charger les stats ici
            var projetRepo = new Repositories.ProjetRepository();
            var tacheRepo = new Repositories.TacheRepository();
            
            int nbProjets = projetRepo.GetByMembre(_utilisateurCourant.Id).Count;
            int nbTaches = tacheRepo.GetAllByAssignee(_utilisateurCourant.Id).Count;
            
            dashboard.SetStats(nbProjets, nbTaches);
            _mainForm.ChargerVue(dashboard);
        }

        public void OuvrirGestionUtilisateurs()
        {
            var utilisateurForm = new UtilisateurForm();
            var utilisateurController = new UtilisateurController(utilisateurForm);
            utilisateurForm.Show();
        }

        public void OuvrirGestionProjets()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            _mainForm.ChargerVue(projetForm);
        }

        public void OuvrirCreationProjet()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            _mainForm.ChargerVue(projetForm);
            
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du projet :", "Nouveau Projet", "");
            if (!string.IsNullOrWhiteSpace(nom))
            {
                projetController.CreerProjet(nom, "");
            }
        }

        public void OuvrirMesTaches()
        {
            // Ouvre le Kanban pour les tâches de l'utilisateur
            // Pour l'instant on ouvre le Kanban du premier projet ou une vue globale
            var projetRepo = new Repositories.ProjetRepository();
            var projets = projetRepo.GetByMembre(_utilisateurCourant.Id);
            
            if (projets.Count > 0)
            {
                var kanbanForm = new KanbanForm();
                var kanbanController = new KanbanController(kanbanForm, projets[0], _utilisateurCourant);
                _mainForm.ChargerVue(kanbanForm);
            }
            else
            {
                MessageBox.Show("Vous n'avez aucun projet pour afficher des tâches.");
            }
        }

        public void OuvrirToutesTaches()
        {
             OuvrirMesTaches(); // On peut affiner plus tard
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