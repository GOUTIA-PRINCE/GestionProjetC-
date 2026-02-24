// =============================================================================
// Contrôleur principal (MainController)
// Orchestre la navigation entre toutes les sections de l'application
// (Dashboard, Projets, Utilisateurs, Mes Tâches, Analyse) et gère
// la déconnexion de l'utilisateur depuis le formulaire principal.
// =============================================================================

using GestionProjet.Models;
using GestionProjet.Views;
using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur principal de l'application, associé au <see cref="MainForm"/>.
    /// Gère toutes les navigations internes (entre les sections de l'app)
    /// et la déconnexion de l'utilisateur courant.
    /// </summary>
    public class MainController : BaseController
    {
        /// <summary>Référence vers le formulaire principal (vue coque).</summary>
        private readonly MainForm _mainForm;

        /// <summary>L'utilisateur actuellement connecté à l'application.</summary>
        private readonly Utilisateur _utilisateurCourant;

        /// <summary>
        /// Initialise le contrôleur principal, enregistre la vue MainForm comme
        /// fenêtre globale de navigation (SPA) et affiche le dashboard au démarrage.
        /// </summary>
        /// <param name="mainForm">Le formulaire principal (coque SPA).</param>
        /// <param name="utilisateur">L'utilisateur connecté.</param>
        public MainController(MainForm mainForm, Utilisateur utilisateur)
        {
            _mainForm = mainForm;
            _utilisateurCourant = utilisateur;
            _mainForm.SetController(this, utilisateur);
            CurrentForm = mainForm;
            // Définit MainView statique pour que tous les contrôleurs puissent
            // naviguer dans le panneau central du MainForm
            BaseController.MainView = mainForm;
            
            // Afficher le dashboard au démarrage de l'application
            AfficherDashboard();
        }

        /// <summary>
        /// Charge et affiche le tableau de bord (DashboardForm) dans le panneau central.
        /// Récupère les statistiques de l'utilisateur courant (nb projets, nb tâches).
        /// </summary>
        public void AfficherDashboard()
        {
            var dashboard = new DashboardForm();

            // Chargement des statistiques depuis les repositories
            var projetRepo = new Repositories.ProjetRepository();
            var tacheRepo = new Repositories.TacheRepository();
            
            // Compte les projets dont l'utilisateur est membre
            int nbProjets = projetRepo.GetByMembre(_utilisateurCourant.Id).Count;
            // Compte les tâches assignées à l'utilisateur
            int nbTaches = tacheRepo.GetAllByAssignee(_utilisateurCourant.Id).Count;
            
            dashboard.SetController(this);
            dashboard.SetStats(nbProjets, nbTaches);
            _mainForm.ChargerVue(dashboard);
        }

        /// <summary>
        /// Ouvre le formulaire de gestion des utilisateurs dans le panneau central.
        /// </summary>
        public void OuvrirGestionUtilisateurs()
        {
            var utilisateurForm = new UtilisateurForm();
            // Le contrôleur utilisateur gère la logique CRUD des utilisateurs
            var utilisateurController = new UtilisateurController(utilisateurForm, _utilisateurCourant);
            _mainForm.ChargerVue(utilisateurForm);
        }

        /// <summary>
        /// Ouvre le formulaire de gestion des projets dans le panneau central.
        /// </summary>
        public void OuvrirGestionProjets()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            _mainForm.ChargerVue(projetForm);
        }

        /// <summary>
        /// Ouvre le tableau Kanban du premier projet de l'utilisateur.
        /// Si l'utilisateur n'a aucun projet, affiche un message et redirige
        /// vers la gestion des projets.
        /// </summary>
        public void OuvrirMesTaches()
        {
            var projetRepo = new Repositories.ProjetRepository();
            var projets = projetRepo.GetByMembre(_utilisateurCourant.Id);
            
            if (projets.Count > 0)
            {
                // Ouvre le Kanban pour le premier projet trouvé
                var kanbanForm = new KanbanForm();
                var kanbanController = new KanbanController(kanbanForm, projets[0], _utilisateurCourant);
                _mainForm.ChargerVue(kanbanForm);
            }
            else
            {
                // Aucun projet disponible → redirige vers la création de projet
                AfficherMessage("Vous n'avez aucun projet pour afficher des tâches.");
                OuvrirGestionProjets();
            }
        }

        /// <summary>
        /// Ouvre le formulaire d'analyse (graphiques et statistiques) dans le panneau central.
        /// </summary>
        public void OuvrirAnalyse()
        {
            var analyseForm = new AnalyseForm();
            var analyseController = new AnalyseController(analyseForm, _utilisateurCourant);
            _mainForm.ChargerVue(analyseForm);
        }

        /// <summary>
        /// Déconnecte l'utilisateur après confirmation.
        /// Ferme le MainForm et réaffiche le formulaire de connexion.
        /// </summary>
        public void Deconnexion()
        {
            if (ConfirmerAction("Voulez-vous vraiment vous déconnecter ?"))
            {
                // Réinitialise la référence statique au MainForm
                BaseController.MainView = null;
                // Signale au MainForm qu'il se ferme volontairement (pas par la croix)
                _mainForm.EstEnDeconnexion = true;
                _mainForm.Close();
                
                // Réafficher le formulaire de login
                var loginForm = new LoginForm();
                var loginController = new LoginController(loginForm);
                loginForm.Show();
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
    }
}