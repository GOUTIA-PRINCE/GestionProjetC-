using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class TacheForm : Form
    {
        private Tache _tache;
        public Tache Tache => _tache;

        public TacheForm(Tache tache, List<Statut> statuts, List<Priorite> priorites, List<Utilisateur> membres)
        {
            InitializeComponent();
            _tache = tache ?? new Tache();

            // Populate ComboBoxes
            cmbStatut.DataSource = statuts;
            cmbStatut.DisplayMember = "Libelle";
            cmbStatut.ValueMember = "Id";

            cmbPriorite.DataSource = priorites;
            cmbPriorite.DisplayMember = "Libelle";
            cmbPriorite.ValueMember = "Id";

            cmbAssignee.DataSource = membres;
            cmbAssignee.DisplayMember = "Nom";
            cmbAssignee.ValueMember = "Id";

            if (tache != null)
            {
                lblFormTitre.Text = "Modifier la tâche";
                txtTitre.Text = tache.Titre;
                txtDescription.Text = tache.Description;
                cmbStatut.SelectedValue = tache.StatutId;
                cmbPriorite.SelectedValue = tache.PrioriteId ?? -1;
                cmbAssignee.SelectedValue = tache.AssigneeId ?? -1;
                if (tache.DateEcheance.HasValue)
                    dtpEcheance.Value = tache.DateEcheance.Value;
            }
            else
            {
                lblFormTitre.Text = "Nouvelle tâche";
                dtpEcheance.Value = DateTime.Now.AddDays(7);
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                MessageBox.Show("Le titre est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _tache.Titre = txtTitre.Text.Trim();
            _tache.Description = txtDescription.Text.Trim();
            _tache.StatutId = (int)cmbStatut.SelectedValue;
            _tache.PrioriteId = (int)cmbPriorite.SelectedValue;
            _tache.AssigneeId = (int)cmbAssignee.SelectedValue;
            _tache.DateEcheance = dtpEcheance.Value;

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
