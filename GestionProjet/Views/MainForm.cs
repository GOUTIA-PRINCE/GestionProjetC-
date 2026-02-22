using System;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class MainForm : Form
    {
        private MainController _controller;
        private Utilisateur _utilisateurCourant;

        public MainForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller, Utilisateur utilisateur)
        {
            _controller = controller;
            _utilisateurCourant = utilisateur;
            lblBienvenue.Text = $"Bienvenue {_utilisateurCourant.Nom} !";
            lblStatus.Text = $"Connecté en tant que {_utilisateurCourant.Email}";
        }

        public void ChargerVue(Form form)
        {
            pnlMainContent.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Add(form);
            form.Show();
        }

        private void menuUtilisateurs_Click(object sender, EventArgs e)
        {
            _controller.OuvrirGestionUtilisateurs();
        }

        private void menuCreerProjet_Click(object sender, EventArgs e)
        {
            _controller.OuvrirCreationProjet();
        }

        private void menuMesTaches_Click(object sender, EventArgs e)
        {
            _controller.OuvrirMesTaches();
        }

        private void menuToutesTaches_Click(object sender, EventArgs e)
        {
            _controller.OuvrirToutesTaches();
        }

        private void menuTache_Click(object sender, EventArgs e)
        {
            // Par défaut, on peut ouvrir toutes les tâches ou juste défiler le menu
            _controller.OuvrirToutesTaches();
        }

        private void menuProjet_Click(object sender, EventArgs e)
        {
            _controller.OuvrirGestionProjets();
        }
        private void menuDashboard_Click(object sender, EventArgs e)
        {
            _controller.AfficherDashboard();
        }

        private void menuDeconnexion_Click(object sender, EventArgs e)
        {
            _controller.Deconnexion();
        }

        private void menuQuitter_Click(object sender, EventArgs e)
        {
            _controller.QuitterApplication();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                _controller.QuitterApplication();
            }
        }
    }
}