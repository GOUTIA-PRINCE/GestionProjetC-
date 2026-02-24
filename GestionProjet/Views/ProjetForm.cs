using System;
using System.Collections.Generic;
using System.Drawing;
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

            // Style headers
            dgvProjets.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181);
            dgvProjets.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProjets.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nom", HeaderText = "PROJET", FillWeight = 25 });
            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "DESCRIPTION", FillWeight = 35 });
            
            // Colonne Progression (Custom Painted)
            var colProg = new DataGridViewTextBoxColumn { DataPropertyName = "Progression", HeaderText = "AVANCEMENT", FillWeight = 15 };
            dgvProjets.Columns.Add(colProg);

            // Bouton Modifier
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.HeaderText = "";
            btnEdit.Text = "Modifier";
            btnEdit.UseColumnTextForButtonValue = true;
            btnEdit.FillWeight = 8;
            btnEdit.FlatStyle = FlatStyle.Flat;
            dgvProjets.Columns.Add(btnEdit);

            // Bouton Supprimer
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "";
            btnDelete.Text = "Supprimer";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.FillWeight = 8;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.DefaultCellStyle.ForeColor = Color.Red;
            dgvProjets.Columns.Add(btnDelete);

            // Bouton pour ouvrir le Kanban
            DataGridViewButtonColumn btnKanban = new DataGridViewButtonColumn();
            btnKanban.HeaderText = "DÉTAILS";
            btnKanban.Text = "Ouvrir";
            btnKanban.UseColumnTextForButtonValue = true;
            btnKanban.FillWeight = 9;
            btnKanban.FlatStyle = FlatStyle.Flat;
            dgvProjets.Columns.Add(btnKanban);

            // Custom painting for the progress bar
            dgvProjets.CellPainting += (s, e) => {
                if (e.RowIndex >= 0 && e.ColumnIndex == 2) // Index de la colonne Progression
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                    
                    int progress = (int)e.Value;
                    int padding = 10;
                    int barWidth = e.CellBounds.Width - (padding * 2);
                    int barHeight = 15;
                    int barX = e.CellBounds.X + padding;
                    int barY = e.CellBounds.Y + (e.CellBounds.Height - barHeight) / 2;

                    // Background
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(230, 230, 230)), barX, barY, barWidth, barHeight);
                    
                    // Progress
                    int progressWidth = (int)(barWidth * (progress / 100.0));
                    Color barColor = progress == 100 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(63, 81, 181);
                    if (progress > 0)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(barColor), barX, barY, progressWidth, barHeight);
                    }

                    // Text percentage
                    string text = $"{progress}%";
                    Font font = new Font("Segoe UI", 8, FontStyle.Bold);
                    Size textSize = TextRenderer.MeasureText(text, font);
                    e.Graphics.DrawString(text, font, Brushes.Black, barX + (barWidth - textSize.Width) / 2, barY - 15);

                    e.Handled = true;
                }
            };
        }

        public void AfficherProjets(List<Projet> projets)
        {
            _projets = projets;
            dgvProjets.DataSource = null;
            dgvProjets.DataSource = _projets;
        }

        private void btnNouveauProjet_Click(object sender, EventArgs e)
        {
            _controller.AjouterProjet();
        }

        private void dgvProjets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var projet = _projets[e.RowIndex];
                string headerText = dgvProjets.Columns[e.ColumnIndex].HeaderText;
                string buttonText = "";
                if (dgvProjets.Columns[e.ColumnIndex] is DataGridViewButtonColumn btnCol)
                {
                    buttonText = btnCol.Text;
                }

                if (buttonText == "Modifier")
                {
                    _controller.ModifierProjet(projet);
                }
                else if (buttonText == "Supprimer")
                {
                    _controller.SupprimerProjet(projet);
                }
                else if (buttonText == "Ouvrir")
                {
                    _controller.OuvrirKanban(projet);
                }
            }
        }
    }
}
