using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class ProjetForm : Form
    {
        private ProjetController _controller;
        private List<Projet> _projets;

        public ProjetForm()
        {
            InitializeComponent();
            ConfigurerDataGridView();
        }

        public void SetController(ProjetController controller)
        {
            _controller = controller;
        }

        private void ConfigurerDataGridView()
        {
            dgvProjets.AutoGenerateColumns = false;
            dgvProjets.Columns.Clear();

            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nom", HeaderText = "Nom du Projet", Width = 200 });
            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", Width = 300 });
            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DateFinPrevue", HeaderText = "Échéance", Width = 100 });

            // Bouton pour ouvrir le Kanban
            DataGridViewButtonColumn btnKanban = new DataGridViewButtonColumn();
            btnKanban.HeaderText = "Action";
            btnKanban.Text = "Ouvrir Kanban";
            btnKanban.UseColumnTextForButtonValue = true;
            btnKanban.Width = 100;
            dgvProjets.Columns.Add(btnKanban);
        }

        public void AfficherProjets(List<Projet> projets)
        {
            _projets = projets;
            dgvProjets.DataSource = null;
            dgvProjets.DataSource = _projets;
        }

        private void btnNouveauProjet_Click(object sender, EventArgs e)
        {
            // Afficher une boîte de dialogue simple ou un panel pour créer un projet
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du projet :", "Nouveau Projet", "");
            if (!string.IsNullOrWhiteSpace(nom))
            {
                _controller.CreerProjet(nom, "");
            }
        }

        private void dgvProjets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProjets.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var projet = _projets[e.RowIndex];
                _controller.OuvrirKanban(projet);
            }
        }
    }
}
