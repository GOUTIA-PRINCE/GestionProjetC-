using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class KanbanForm : Form
    {
        private KanbanController _controller;
        private Projet _projet;

        public KanbanForm()
        {
            InitializeComponent();
        }

        public void SetController(KanbanController controller, Projet projet)
        {
            _controller = controller;
            _projet = projet;
            lblProjetTitre.Text = $"Kanban : {_projet.Nom}";
        }

        public void InitialiserColonnes(List<Statut> statuts)
        {
            pnlKanban.Controls.Clear();
            pnlKanban.ColumnCount = statuts.Count;
            
            // Configuration des colonnes dynamiquement
            float percent = 100f / statuts.Count;
            pnlKanban.ColumnStyles.Clear();

            for (int i = 0; i < statuts.Count; i++)
            {
                pnlKanban.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
                
                var col = CreateColumnControl(statuts[i]);
                pnlKanban.Controls.Add(col, i, 0);
            }
        }

        private Control CreateColumnControl(Statut statut)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Tag = statut.Id,
                Padding = new Padding(5)
            };

            var header = new Label
            {
                Text = statut.Libelle.ToUpper(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 200,
                Padding = new Padding(5),
                BackColor = Color.LightGray,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            container.Controls.Add(panel);
            container.Controls.Add(header);
            header.Dock = DockStyle.Top;
            panel.Dock = DockStyle.Fill;

            return container;
        }

        public void AfficherTaches(List<Tache> taches)
        {
            // Vider les listes existantes (les FlowLayoutPanels sont dans les containers)
            foreach (Control container in pnlKanban.Controls)
            {
                var flow = (FlowLayoutPanel)container.Controls[0];
                flow.Controls.Clear();
            }

            foreach (var tache in taches)
            {
                var card = CreateTaskCard(tache);
                // Trouver le bon flow panel par l'ID du statut
                foreach (Control container in pnlKanban.Controls)
                {
                    var flow = (FlowLayoutPanel)container.Controls[0];
                    if ((int)flow.Tag == tache.StatutId)
                    {
                        flow.Controls.Add(card);
                        break;
                    }
                }
            }
        }

        private Control CreateTaskCard(Tache tache)
        {
            var card = new Panel
            {
                Width = 180,
                Height = 100,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10),
                Cursor = Cursors.Hand
            };

            // Barre de priorité
            var colorBar = new Panel
            {
                Height = 4,
                Dock = DockStyle.Top,
                BackColor = string.IsNullOrEmpty(tache.Priorite?.CouleurHex) 
                            ? Color.Gray 
                            : ColorTranslator.FromHtml(tache.Priorite.CouleurHex)
            };
            card.Controls.Add(colorBar);

            var lblTitre = new Label
            {
                Text = tache.Titre,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(5, 10),
                Width = 170,
                AutoEllipsis = true
            };
            card.Controls.Add(lblTitre);

            var lblUser = new Label
            {
                Text = tache.Assignee?.Nom ?? "Non assigné",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                Location = new Point(5, 75),
                Width = 170,
                ForeColor = Color.DimGray
            };
            card.Controls.Add(lblUser);

            card.Click += (s, e) => _controller.ModifierTache(tache);

            return card;
        }

        private void btnAjouterTache_Click(object sender, EventArgs e)
        {
            _controller.AjouterTache();
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            _controller.RetourProjets();
        }
    }
}
