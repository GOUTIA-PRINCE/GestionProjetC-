// =============================================================================
// Vue principale (MainForm) - Coque SPA (Single Page Application)
// Ce formulaire est la fenêtre principale de l'application après connexion.
// Il contient :
//   - Une barre latérale (sidebar) avec des boutons de navigation
//   - Un panneau central (pnlMainContent) qui charge les sous-vues
// La navigation charge les différentes vues comme des sous-formulaires
// dans le panneau central, simulant une navigation SPA sans ouvrir
// de nouvelles fenêtres Windows.
// =============================================================================

using System;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire principal de l'application (coque SPA).
    /// Héberge toutes les vues dans son panneau central (<c>pnlMainContent</c>).
    /// La barre latérale (sidebar) permet la navigation entre les sections.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>Référence vers le contrôleur principal.</summary>
        private MainController _controller;

        /// <summary>L'utilisateur actuellement connecté (affiché dans la sidebar).</summary>
        private Utilisateur _utilisateurCourant;

        /// <summary>
        /// Drapeau indiquant si la fermeture du formulaire est initiée par
        /// une déconnexion volontaire. Si false, la croix déclenche "Quitter".
        /// </summary>
        public bool EstEnDeconnexion { get; set; } = false;

        /// <summary>
        /// Initialise le formulaire principal et active le glisser-déposer.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            // Active le déplacement de la fenêtre sans bordure
            SetupDragging();
        }

        /// <summary>
        /// Active le déplacement de la fenêtre principale par drag-and-drop.
        /// Plusieurs éléments permettent le drag (en-tête et logo) pour couvrir
        /// toutes les zones cliquables de la barre supérieure.
        /// </summary>
        private void SetupDragging()
        {
            Point lastLocation = Point.Empty;
            bool isMouseDown = false;

            // Actions lambda réutilisées sur plusieurs contrôles
            Action<object, MouseEventArgs> mouseDown = (s, e) => {
                isMouseDown = true;
                lastLocation = e.Location;
            };

            Action<object, MouseEventArgs> mouseMove = (s, e) => {
                if (isMouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X,
                        (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            };

            Action<object, MouseEventArgs> mouseUp = (s, e) => isMouseDown = false;

            // Application sur le panneau d'en-tête et le label logo
            pnlHeader.MouseDown += (s, e) => mouseDown(s, e);
            pnlHeader.MouseMove += (s, e) => mouseMove(s, e);
            pnlHeader.MouseUp += (s, e) => mouseUp(s, e);

            lblLogo.MouseDown += (s, e) => mouseDown(s, e);
            lblLogo.MouseMove += (s, e) => mouseMove(s, e);
            lblLogo.MouseUp += (s, e) => mouseUp(s, e);
        }

        /// <summary>
        /// Injecte le contrôleur et l'utilisateur connecté dans la vue.
        /// Met à jour l'affichage de la sidebar avec le nom et l'email de l'utilisateur.
        /// Active le bouton Dashboard par défaut.
        /// </summary>
        /// <param name="controller">Le contrôleur principal associé à cette vue.</param>
        /// <param name="utilisateur">L'utilisateur actuellement connecté.</param>
        public void SetController(MainController controller, Utilisateur utilisateur)
        {
            _controller = controller;
            _utilisateurCourant = utilisateur;
            // Affichage du message de bienvenue et de l'email dans la sidebar
            lblBienvenue.Text = $"Bienvenue, {_utilisateurCourant.Nom} !";
            lblStatus.Text = _utilisateurCourant.Email;
            
            // Activation visuelle du bouton Dashboard au démarrage
            SetActiveButton(btnDashboard);
        }

        /// <summary>
        /// Met en surbrillance le bouton de navigation actif dans la sidebar.
        /// Réinitialise tous les boutons à leur couleur par défaut puis
        /// applique la couleur primaire au bouton sélectionné.
        /// </summary>
        /// <param name="btn">Le bouton à mettre en surbrillance.</param>
        private void SetActiveButton(Button btn)
        {
            // Réinitialisation de tous les boutons de la sidebar
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button b)
                {
                    b.BackColor = Color.FromArgb(33, 37, 41); // Couleur sidebar par défaut
                    b.ForeColor = Color.White;
                }
            }
            // Mise en surbrillance du bouton actif (couleur primaire)
            btn.BackColor = Color.FromArgb(63, 81, 181);
            btn.ForeColor = Color.White;
        }

        /// <summary>
        /// Charge une vue (sous-formulaire) dans le panneau central.
        /// Vide d'abord le panneau, configure le formulaire en mode "enfant"
        /// (sans bordure, remplissant toute la surface), puis l'affiche.
        /// C'est le mécanisme central de la navigation SPA.
        /// </summary>
        /// <param name="form">La vue à afficher dans le panneau central.</param>
        public void ChargerVue(Form form)
        {
            pnlMainContent.Controls.Clear(); // Vide la vue précédente
            form.TopLevel = false;            // Désactive le mode fenêtre indépendante
            form.FormBorderStyle = FormBorderStyle.None; // Supprime la bordure Windows
            form.Dock = DockStyle.Fill;       // Remplit tout le panneau disponible
            pnlMainContent.Controls.Add(form);
            form.Show();
        }

        // ── Gestionnaires d'événements des boutons de la sidebar ─────────────

        /// <summary>Navigue vers la section "Utilisateurs".</summary>
        private void menuUtilisateurs_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnUtilisateurs);
            _controller.OuvrirGestionUtilisateurs();
        }

        /// <summary>Navigue vers la section "Analyse".</summary>
        private void menuAnalyse_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAnalyse);
            _controller.OuvrirAnalyse();
        }

        /// <summary>Navigue vers la section "Mes Tâches" (Kanban du premier projet).</summary>
        private void menuMesTaches_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTaches);
            _controller.OuvrirMesTaches();
        }

        /// <summary>Navigue vers la section "Projets".</summary>
        private void menuProjet_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnProjets);
            _controller.OuvrirGestionProjets();
        }

        /// <summary>Navigue vers le tableau de bord (Dashboard).</summary>
        private void menuDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            _controller.AfficherDashboard();
        }

        /// <summary>Déclenche la déconnexion depuis le menu contextuel.</summary>
        private void menuDeconnexion_Click(object sender, EventArgs e)
        {
            _controller.Deconnexion();
        }

        /// <summary>Déclenche la déconnexion depuis le lien de la sidebar.</summary>
        private void lnkDeconnexion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _controller.Deconnexion();
        }

        /// <summary>
        /// Intercepte la fermeture du formulaire :
        /// - Si la fermeture est déclenchée par <see cref="EstEnDeconnexion"/> = true,
        ///   la fermeture est autorisée (retour au login géré par le contrôleur).
        /// - Si l'utilisateur clique sur la croix sans se déconnecter, 
        ///   la fermeture est annulée et le contrôleur demande confirmation de quitter.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Si l'utilisateur ferme en cliquant sur la croix (pas via déconnexion)
            if (e.CloseReason == CloseReason.UserClosing && !EstEnDeconnexion)
            {
                e.Cancel = true; // Annule la fermeture
                _controller.QuitterApplication(); // Affiche la confirmation de quitter
            }
        }
    }
}