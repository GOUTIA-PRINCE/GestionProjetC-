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

        private void menuUtilisateurs_Click(object sender, EventArgs e)
        {
            _controller.OuvrirGestionUtilisateurs();
        }

        private void menuProjets_Click(object sender, EventArgs e)
        {
            _controller.OuvrirGestionProjets();
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