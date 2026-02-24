using System;
using System.Drawing;
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
            SetupDragging();
        }

        private void SetupDragging()
        {
            Point lastLocation = Point.Empty;
            bool isMouseDown = false;

            pnlHeader.MouseDown += (s, e) => {
                isMouseDown = true;
                lastLocation = e.Location;
            };

            pnlHeader.MouseMove += (s, e) => {
                if (isMouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X,
                        (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            };

            pnlHeader.MouseUp += (s, e) => isMouseDown = false;
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
