using System;
using System.Windows.Forms;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class ProjetEditForm : Form
    {
        private Projet _projet;
        public Projet Projet => _projet;

        public ProjetEditForm(Projet projet = null)
        {
            InitializeComponent();
            _projet = projet ?? new Projet();

            if (projet != null)
            {
                lblTitre.Text = "Modifier le Projet";
                txtNom.Text = projet.Nom;
                txtDescription.Text = projet.Description;
                if (projet.DateFinPrevue.HasValue)
                    dtpDateFin.Value = projet.DateFinPrevue.Value;
            }
            else
            {
                lblTitre.Text = "Nouveau Projet";
                dtpDateFin.Value = DateTime.Now.AddMonths(1);
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom du projet est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _projet.Nom = txtNom.Text.Trim();
            _projet.Description = txtDescription.Text.Trim();
            _projet.DateFinPrevue = dtpDateFin.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
