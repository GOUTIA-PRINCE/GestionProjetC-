// =============================================================================
// Vue - Tableau de bord (DashboardForm)
// Affiche les statistiques clés de l'utilisateur connecté :
//   - Nombre de projets dont il est membre
//   - Nombre de tâches qui lui sont assignées
// Les cartes statistiques sont cliquables pour naviguer directement
// vers la section correspondante (Projets ou Mes Tâches).
// =============================================================================

using System;
using System.Windows.Forms;
using GestionProjet.Controllers;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire du tableau de bord. Affiche un résumé statistique rapide
    /// et permet une navigation directe vers les sections principales via
    /// des cartes cliquables.
    /// </summary>
    public partial class DashboardForm : Form
    {
        /// <summary>
        /// Référence vers le contrôleur principal pour la navigation.
        /// Utilisée en mode null-conditionnel (?.) car SetController peut être
        /// appelé après SetupInteractivity.
        /// </summary>
        private MainController _controller;

        /// <summary>
        /// Initialise le tableau de bord et configure l'interactivité des cartes.
        /// </summary>
        public DashboardForm()
        {
            InitializeComponent();
            // Configure les cartes statistiques comme cliquables (avec curseur main)
            SetupInteractivity();
        }

        /// <summary>
        /// Enregistre le contrôleur principal pour permettre la navigation
        /// depuis les cartes statistiques du dashboard.
        /// </summary>
        /// <param name="controller">Le contrôleur principal de l'application.</param>
        public void SetController(MainController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Configure les cartes statistiques pour qu'elles soient cliquables.
        /// - La carte "Projets" et ses labels naviguent vers la section Projets.
        /// - La carte "Tâches" et ses labels naviguent vers la section Mes Tâches.
        /// L'opérateur null-conditionnel (?.) évite une NullReferenceException
        /// si le contrôleur n'est pas encore assigné.
        /// </summary>
        private void SetupInteractivity()
        {
            // Curseur "main" pour indiquer que les panneaux sont cliquables
            pnlProjets.Cursor = Cursors.Hand;
            pnlTaches.Cursor = Cursors.Hand;

            // Clic sur la carte Projets → navigue vers la gestion des projets
            pnlProjets.Click += (s, e) => _controller?.OuvrirGestionProjets();
            lblProjetsCount.Click += (s, e) => _controller?.OuvrirGestionProjets();
            lblProjetsTitle.Click += (s, e) => _controller?.OuvrirGestionProjets();

            // Clic sur la carte Tâches → navigue vers "Mes Tâches" (Kanban)
            pnlTaches.Click += (s, e) => _controller?.OuvrirMesTaches();
            lblTachesCount.Click += (s, e) => _controller?.OuvrirMesTaches();
            lblTachesTitle.Click += (s, e) => _controller?.OuvrirMesTaches();
        }

        /// <summary>
        /// Met à jour les compteurs affichés dans les cartes statistiques.
        /// Appelé par le contrôleur principal après avoir récupéré les données.
        /// </summary>
        /// <param name="nbProjets">Nombre de projets dont l'utilisateur est membre.</param>
        /// <param name="nbTaches">Nombre de tâches assignées à l'utilisateur.</param>
        public void SetStats(int nbProjets, int nbTaches)
        {
            lblProjetsCount.Text = nbProjets.ToString();
            lblTachesCount.Text = nbTaches.ToString();
        }
    }
}
