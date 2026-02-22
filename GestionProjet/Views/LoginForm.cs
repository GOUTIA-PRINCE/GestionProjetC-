
using System;
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

        // Gestion de l'événement Enter pour faciliter la saisie
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnConnexion_Click(this, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
