using System;
using System.Drawing;
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
            SetupDragging();
        }

        private void SetupDragging()
        {
            Point lastLocation = Point.Empty;
            bool isMouseDown = false;

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

            pnlHeader.MouseDown += (s, e) => mouseDown(s, e);
            pnlHeader.MouseMove += (s, e) => mouseMove(s, e);
            pnlHeader.MouseUp += (s, e) => mouseUp(s, e);

            lblLogo.MouseDown += (s, e) => mouseDown(s, e);
            lblLogo.MouseMove += (s, e) => mouseMove(s, e);
            lblLogo.MouseUp += (s, e) => mouseUp(s, e);
        }

        public void SetController(MainController controller, Utilisateur utilisateur)
        {
            _controller = controller;
            _utilisateurCourant = utilisateur;
            lblBienvenue.Text = $"Bienvenue, {_utilisateurCourant.Nom} !";
            lblStatus.Text = _utilisateurCourant.Email;
            
            // Set initial button active state
            SetActiveButton(btnDashboard);
        }

        private void SetActiveButton(Button btn)
        {
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button b)
                {
                    b.BackColor = Color.FromArgb(33, 37, 41);
                    b.ForeColor = Color.White;
                }
            }
            btn.BackColor = Color.FromArgb(63, 81, 181);
            btn.ForeColor = Color.White;
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
            SetActiveButton(btnUtilisateurs);
            _controller.OuvrirGestionUtilisateurs();
        }

        private void menuAnalyse_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAnalyse);
            _controller.OuvrirAnalyse();
        }

        private void menuMesTaches_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTaches);
            _controller.OuvrirMesTaches();
        }

        private void menuProjet_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnProjets);
            _controller.OuvrirGestionProjets();
        }

        private void menuDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            _controller.AfficherDashboard();
        }

        private void menuDeconnexion_Click(object sender, EventArgs e)
        {
            _controller.Deconnexion();
        }

        private void lnkDeconnexion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _controller.Deconnexion();
        }

        public bool EstEnDeconnexion { get; set; } = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing && !EstEnDeconnexion)
            {
                e.Cancel = true;
                _controller.QuitterApplication();
            }
        }
    }
}