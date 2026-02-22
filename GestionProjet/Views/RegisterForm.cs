
using System;
using System.Windows.Forms;
using GestionProjet.Controllers;

namespace GestionProjet.Views
{
    public partial class RegisterForm : Form
    {
        private RegisterController _controller;

        public RegisterForm()
        {
            InitializeComponent();
        }

        public void SetController(RegisterController controller)
        {
            _controller = controller;
        }

        private void btnSInscrire_Click(object sender, EventArgs e)
        {
            if (txtMotDePasse.Text != txtConfirmerMotDePasse.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _controller.Inscrire(txtNom.Text.Trim(), txtEmail.Text.Trim(), txtMotDePasse.Text);
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            _controller.RetourLogin();
        }
    }
}
