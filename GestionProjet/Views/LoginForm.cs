// =============================================================================
// Vue - Formulaire de connexion (LoginForm)
// Affiche la page de login avec les champs email et mot de passe.
// Ce formulaire est sans bordure (borderless) et peut être déplacé via
// le panneau de branding (drag personnalisé).
// La pression de la touche Entrée déclenche la connexion.
// =============================================================================

using System;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire de connexion de l'application.
    /// Vue pasive (MVC) qui délègue toute logique métier au <see cref="LoginController"/>.
    /// Le formulaire est sans bordure Windows et peut être déplacé à la souris
    /// via le panneau de branding (pnlBranding).
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Référence vers le contrôleur associé à cette vue.
        /// Injectée via <see cref="SetController"/>.
        /// </summary>
        private LoginController _controller;

        /// <summary>
        /// Initialise le formulaire et active le glisser-déposer de la fenêtre.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            // Active le déplacement personnalisé (la fenêtre est sans bordure)
            SetupDragging();
        }

        /// <summary>
        /// Active le déplacement de la fenêtre sans bordure par drag-and-drop
        /// sur le panneau de branding (pnlBranding).
        /// Fonctionne en suivant les événements MouseDown, MouseMove, MouseUp.
        /// </summary>
        private void SetupDragging()
        {
            // Position de la souris lors du dernier MouseDown
            Point lastLocation = Point.Empty;
            bool isMouseDown = false;

            // Enregistrement de la position initiale du clic
            pnlBranding.MouseDown += (s, e) => {
                isMouseDown = true;
                lastLocation = e.Location;
            };

            // Déplacement de la fenêtre selon le delta de la souris
            pnlBranding.MouseMove += (s, e) => {
                if (isMouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X,
                        (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            };

            // Fin du déplacement lors du relâchement du clic
            pnlBranding.MouseUp += (s, e) => isMouseDown = false;
        }

        /// <summary>
        /// Enregistre le contrôleur associé à cette vue.
        /// Appelé par le <see cref="LoginController"/> au moment de sa création.
        /// </summary>
        /// <param name="controller">Le contrôleur de login.</param>
        public void SetController(LoginController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "Se connecter".
        /// Passe les champs email et mot de passe au contrôleur pour authentification.
        /// Le Trim() supprime les espaces parasites dans l'email.
        /// </summary>
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            _controller.Authentifier(txtEmail.Text.Trim(), txtMotDePasse.Text);
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "Quitter".
        /// Demande confirmation via le contrôleur avant de quitter l'application.
        /// </summary>
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            _controller.QuitterApplication();
        }

        /// <summary>
        /// Raccourci clavier : la touche Entrée déclenche le bouton de connexion,
        /// ce qui améliore l'ergonomie du formulaire.
        /// </summary>
        /// <param name="msg">Message Windows intercepté.</param>
        /// <param name="keyData">Touches pressées.</param>
        /// <returns>true si la touche Entrée a été traitée, false sinon.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // Simule un clic sur le bouton de connexion via la touche Entrée
                btnConnexion_Click(this, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Déclenché lors du clic sur le lien "Créer un compte".
        /// Navigue vers le formulaire d'inscription via le contrôleur.
        /// </summary>
        private void lnkCreerCompte_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _controller.AfficherPageInscription();
        }
    }
}
