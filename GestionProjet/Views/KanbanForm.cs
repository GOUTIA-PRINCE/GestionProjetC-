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
                BackColor = Color.FromArgb(240, 242, 245),
                Tag = statut, // Stocker l'objet statut complet
                Padding = new Padding(10),
                AllowDrop = true
            };

            // Event handlers for Drop
            panel.DragEnter += (s, e) => {
                if (e.Data.GetDataPresent(typeof(Tache)))
                    e.Effect = DragDropEffects.Move;
            };

            panel.DragDrop += (s, e) => {
                var tache = (Tache)e.Data.GetData(typeof(Tache));
                var targetStatut = (Statut)((FlowLayoutPanel)s).Tag;
                
                if (tache.StatutId != targetStatut.Id)
                {
                    _controller.ChangerStatutTache(tache, targetStatut.Id);
                }
            };

            var header = new Label
            {
                Text = statut.Libelle.ToUpper(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 200,
                Padding = new Padding(10),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(63, 81, 181),
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 40
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
            foreach (Control container in pnlKanban.Controls)
            {
                var flow = (FlowLayoutPanel)container.Controls[0];
                flow.Controls.Clear();
            }

            foreach (var tache in taches)
            {
                var card = CreateTaskCard(tache);
                foreach (Control container in pnlKanban.Controls)
                {
                    var flow = (FlowLayoutPanel)container.Controls[0];
                    if (((Statut)flow.Tag).Id == tache.StatutId)
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
                Width = 220,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Custom border drawing for rounded effect or just flat
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            };

            var colorBar = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                BackColor = string.IsNullOrEmpty(tache.Priorite?.CouleurHex) 
                            ? Color.Gray 
                            : ColorTranslator.FromHtml(tache.Priorite.CouleurHex)
            };
            card.Controls.Add(colorBar);

            var lblTitre = new Label
            {
                Text = tache.Titre,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 10),
                Width = 190,
                Height = 40,
                AutoEllipsis = true
            };
            card.Controls.Add(lblTitre);

            var lblUser = new Label
            {
                Text = tache.Assignee?.Nom ?? "Non assigné",
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, 80),
                Width = 130,
                ForeColor = Color.DimGray
            };
            card.Controls.Add(lblUser);

            // Drag support with movement threshold
            Point dragStartPoint = Point.Empty;
            card.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Left)
                    dragStartPoint = e.Location;
            };

            card.MouseMove += (s, e) => {
                if (e.Button == MouseButtons.Left && dragStartPoint != Point.Empty)
                {
                    int dragThreshold = 5;
                    if (Math.Abs(e.X - dragStartPoint.X) > dragThreshold || Math.Abs(e.Y - dragStartPoint.Y) > dragThreshold)
                    {
                        card.DoDragDrop(tache, DragDropEffects.Move);
                        dragStartPoint = Point.Empty;
                    }
                }
            };

            card.MouseUp += (s, e) => dragStartPoint = Point.Empty;

            // Simple click for details as requested ("lorsque je clique")
            Action openDetails = () => _controller.ModifierTache(tache);
            
            card.Click += (s, e) => openDetails();
            lblTitre.Click += (s, e) => openDetails();
            lblUser.Click += (s, e) => openDetails();
            colorBar.Click += (s, e) => openDetails();

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
