using System;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;

namespace GestionProjet.Views
{
    public partial class LoginForm : Form
    {
        private LoginController _controller;

        public LoginForm()
        {
            InitializeComponent();
            SetupDragging();
        }

        private void SetupDragging()
        {
            Point lastLocation = Point.Empty;
            bool isMouseDown = false;

            pnlBranding.MouseDown += (s, e) => {
                isMouseDown = true;
                lastLocation = e.Location;
            };

            pnlBranding.MouseMove += (s, e) => {
                if (isMouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X,
                        (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            };

            pnlBranding.MouseUp += (s, e) => isMouseDown = false;
        }

        public void SetController(LoginController controller)
        {
            _controller = controller;
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            _controller.Authentifier(txtEmail.Text.Trim(), txtMotDePasse.Text);
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            _controller.QuitterApplication();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnConnexion_Click(this, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void lnkCreerCompte_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _controller.AfficherPageInscription();
        }
    }
}
