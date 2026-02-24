// =============================================================================
// Vue - Liste des projets (ProjetForm)
// Affiche tous les projets dont l'utilisateur est membre dans un DataGridView.
// Fonctionnalités :
//   - Affichage du nom, description et avancement (barre de progression custom)
//   - Boutons Modifier, Supprimer, Ouvrir (Kanban) par ligne
//   - Dessin personnalisé (CellPainting) de la barre d'avancement en pourcentage
// =============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire d'affichage et de gestion des projets.
    /// Utilise un <see cref="DataGridView"/> avec colonnes personnalisées et
    /// un dessin custom pour la barre de progression (CellPainting).
    /// </summary>
    public partial class ProjetForm : Form
    {
        /// <summary>Référence vers le contrôleur des projets.</summary>
        private ProjetController _controller;

        /// <summary>
        /// Cache local de la liste des projets affichés.
        /// Utilisé pour récupérer le projet cliqué depuis l'index de la ligne DataGridView.
        /// </summary>
        private List<Projet> _projets;

        /// <summary>
        /// Initialise le formulaire et configure les colonnes du DataGridView.
        /// </summary>
        public ProjetForm()
        {
            InitializeComponent();
            ConfigurerDataGridView();
        }

        /// <summary>
        /// Enregistre le contrôleur associé à cette vue.
        /// </summary>
        /// <param name="controller">Le contrôleur des projets.</param>
        public void SetController(ProjetController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Configure les colonnes du DataGridView de manière programmatique :
        /// - Désactive la génération automatique de colonnes
        /// - Ajoute les colonnes Nom, Description, Avancement (avec barre custom)
        /// - Ajoute les colonnes boutons : Modifier, Supprimer, Ouvrir (Kanban)
        /// - Enregistre un événement CellPainting pour le dessin custom de la progression
        /// </summary>
        private void ConfigurerDataGridView()
        {
            // Empêche la génération automatique de colonnes depuis le DataSource
            dgvProjets.AutoGenerateColumns = false;
            dgvProjets.Columns.Clear();

            // Style des en-têtes : fond primaire bleu indigo, texte blanc
            dgvProjets.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181);
            dgvProjets.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProjets.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Colonne Nom du projet
            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "PROJET",
                FillWeight = 25
            });

            // Colonne Description
            dgvProjets.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "DESCRIPTION",
                FillWeight = 35
            });

            // Colonne Progression : le contenu réel est dessiné custom dans CellPainting
            var colProg = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Progression",
                HeaderText = "AVANCEMENT",
                FillWeight = 15
            };
            dgvProjets.Columns.Add(colProg);

            // Bouton "Modifier" (bleu)
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.HeaderText = "";
            btnEdit.Text = "Modifier";
            btnEdit.UseColumnTextForButtonValue = true; // Affiche le texte de la colonne sur chaque cellule
            btnEdit.FillWeight = 8;
            btnEdit.FlatStyle = FlatStyle.Flat;
            dgvProjets.Columns.Add(btnEdit);

            // Bouton "Supprimer" (rouge via ForeColor)
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "";
            btnDelete.Text = "Supprimer";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.FillWeight = 8;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.DefaultCellStyle.ForeColor = Color.Red;
            dgvProjets.Columns.Add(btnDelete);

            // Bouton "Ouvrir" pour accéder au tableau Kanban du projet
            DataGridViewButtonColumn btnKanban = new DataGridViewButtonColumn();
            btnKanban.HeaderText = "DÉTAILS";
            btnKanban.Text = "Ouvrir";
            btnKanban.UseColumnTextForButtonValue = true;
            btnKanban.FillWeight = 9;
            btnKanban.FlatStyle = FlatStyle.Flat;
            dgvProjets.Columns.Add(btnKanban);

            // ── Dessin personnalisé de la barre de progression (colonne index 2) ──
            dgvProjets.CellPainting += (s, e) => {
                // Applique uniquement sur les cellules de données de la colonne Progression (index 2)
                if (e.RowIndex >= 0 && e.ColumnIndex == 2)
                {
                    // Dessine le fond standard de la cellule (sans le texte standard)
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                    int progress = (int)e.Value;
                    int padding = 10;
                    int barWidth = e.CellBounds.Width - (padding * 2);
                    int barHeight = 15;
                    int barX = e.CellBounds.X + padding;
                    // Centre verticalement la barre dans la cellule
                    int barY = e.CellBounds.Y + (e.CellBounds.Height - barHeight) / 2;

                    // Fond gris de la barre (arrière-plan vide)
                    e.Graphics.FillRectangle(
                        new SolidBrush(Color.FromArgb(230, 230, 230)),
                        barX, barY, barWidth, barHeight);

                    // Barre de progression : verte si 100%, bleue sinon
                    int progressWidth = (int)(barWidth * (progress / 100.0));
                    Color barColor = progress == 100
                        ? Color.FromArgb(40, 167, 69)   // Vert : projet terminé
                        : Color.FromArgb(63, 81, 181);   // Bleu : en cours
                    if (progress > 0)
                    {
                        e.Graphics.FillRectangle(
                            new SolidBrush(barColor),
                            barX, barY, progressWidth, barHeight);
                    }

                    // Texte du pourcentage centré au-dessus de la barre
                    string text = $"{progress}%";
                    Font font = new Font("Segoe UI", 8, FontStyle.Bold);
                    Size textSize = TextRenderer.MeasureText(text, font);
                    e.Graphics.DrawString(text, font, Brushes.Black,
                        barX + (barWidth - textSize.Width) / 2,
                        barY - 15);

                    // Signale que la cellule a été dessinée manuellement
                    e.Handled = true;
                }
            };
        }

        /// <summary>
        /// Remplace la source de données du DataGridView par la nouvelle liste de projets.
        /// La réassignation (null puis _projets) force le rafraîchissement complet de la grille.
        /// </summary>
        /// <param name="projets">La liste des projets à afficher.</param>
        public void AfficherProjets(List<Projet> projets)
        {
            _projets = projets;
            // Double assignation pour forcer la mise à jour complète du DataGridView
            dgvProjets.DataSource = null;
            dgvProjets.DataSource = _projets;
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "Nouveau Projet".
        /// Délègue la création au contrôleur.
        /// </summary>
        private void btnNouveauProjet_Click(object sender, EventArgs e)
        {
            _controller.AjouterProjet();
        }

        /// <summary>
        /// Gère les clics sur les boutons d'action de chaque ligne du DataGridView.
        /// Identifie le bouton cliqué par son texte et appelle l'action correspondante.
        /// </summary>
        /// <param name="sender">Le DataGridView source.</param>
        /// <param name="e">L'index de la ligne et de la colonne cliquées.</param>
        private void dgvProjets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore les clics sur l'en-tête (RowIndex = -1)
            if (e.RowIndex >= 0)
            {
                // Récupère le projet correspondant à la ligne cliquée via le cache local
                var projet = _projets[e.RowIndex];
                string buttonText = "";

                // Récupère le texte du bouton de la colonne cliquée
                if (dgvProjets.Columns[e.ColumnIndex] is DataGridViewButtonColumn btnCol)
                {
                    buttonText = btnCol.Text;
                }

                // Dispatch en fonction du bouton cliqué
                if (buttonText == "Modifier")
                    _controller.ModifierProjet(projet);
                else if (buttonText == "Supprimer")
                    _controller.SupprimerProjet(projet);
                else if (buttonText == "Ouvrir")
                    _controller.OuvrirKanban(projet);
            }
        }
    }
}
