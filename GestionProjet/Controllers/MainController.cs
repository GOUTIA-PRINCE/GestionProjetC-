
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
            
            dashboard.SetController(this);
            dashboard.SetStats(nbProjets, nbTaches);
            _mainForm.ChargerVue(dashboard);
        }

        public void OuvrirGestionUtilisateurs()
        {
            var utilisateurForm = new UtilisateurForm();
            var utilisateurController = new UtilisateurController(utilisateurForm, _utilisateurCourant);
            _mainForm.ChargerVue(utilisateurForm);
        }

        public void OuvrirGestionProjets()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            _mainForm.ChargerVue(projetForm);
        }

        public void OuvrirMesTaches()
        {
            // Ouvre le Kanban pour les tâches de l'utilisateur sur son premier projet
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
                AfficherMessage("Vous n'avez aucun projet pour afficher des tâches.");
                OuvrirGestionProjets();
            }
        }

        public void OuvrirAnalyse()
        {
            var analyseForm = new AnalyseForm();
            var analyseController = new AnalyseController(analyseForm, _utilisateurCourant);
            _mainForm.ChargerVue(analyseForm);
        }

        public void Deconnexion()
        {
            if (ConfirmerAction("Voulez-vous vraiment vous déconnecter ?"))
            {
                BaseController.MainView = null;
                _mainForm.EstEnDeconnexion = true;
                _mainForm.Close();
                
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