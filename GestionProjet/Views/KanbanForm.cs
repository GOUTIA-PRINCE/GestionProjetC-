// =============================================================================
// Vue - Tableau Kanban (KanbanForm)
// Affiche les tâches d'un projet organisées en colonnes par statut.
// Fonctionnalités :
//   - Génération dynamique des colonnes selon les statuts de la base de données
//   - Affichage de cartes de tâches avec barre de couleur selon la priorité
//   - Drag-and-drop entre colonnes pour changer le statut d'une tâche
//   - Clic sur une carte → ouverture du formulaire de modification (détails)
//   - Bouton "×" sur chaque carte pour supprimer la tâche directement
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
    /// Formulaire du tableau Kanban.
    /// Affiche les tâches d'un projet en colonnes selon leurs statuts.
    /// Supporte le drag-and-drop pour déplacer des tâches entre colonnes
    /// et les clics pour consulter / modifier le détail d'une tâche.
    /// </summary>
    public partial class KanbanForm : Form
    {
        /// <summary>Référence vers le contrôleur Kanban.</summary>
        private KanbanController _controller;

        /// <summary>Le projet dont ce Kanban affiche les tâches.</summary>
        private Projet _projet;

        /// <summary>
        /// Initialise le formulaire Kanban.
        /// </summary>
        public KanbanForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enregistre le contrôleur et le projet, puis met à jour le titre du formulaire.
        /// </summary>
        /// <param name="controller">Le contrôleur Kanban associé à cette vue.</param>
        /// <param name="projet">Le projet affiché dans ce tableau Kanban.</param>
        public void SetController(KanbanController controller, Projet projet)
        {
            _controller = controller;
            _projet = projet;
            // Affichage du nom du projet dans l'en-tête du Kanban
            lblProjetTitre.Text = $"Kanban : {_projet.Nom}";
        }

        /// <summary>
        /// Crée les colonnes du tableau Kanban de manière dynamique
        /// à partir de la liste des statuts récupérés en base de données.
        /// Chaque statut correspond à une colonne de largeur égale.
        /// </summary>
        /// <param name="statuts">Liste des statuts ordonnés (colonnes du Kanban).</param>
        public void InitialiserColonnes(List<Statut> statuts)
        {
            pnlKanban.Controls.Clear();
            // Nombre de colonnes = nombre de statuts
            pnlKanban.ColumnCount = statuts.Count;

            // Calcul du pourcentage de largeur équitable pour chaque colonne
            float percent = 100f / statuts.Count;
            pnlKanban.ColumnStyles.Clear();

            for (int i = 0; i < statuts.Count; i++)
            {
                // Toutes les colonnes ont la même largeur en pourcentage
                pnlKanban.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));

                // Création du conteneur visuel de la colonne
                var col = CreateColumnControl(statuts[i]);
                pnlKanban.Controls.Add(col, i, 0);
            }
        }

        /// <summary>
        /// Crée le contrôle visuel d'une colonne Kanban pour un statut donné.
        /// Compose un conteneur avec :
        /// - Un label d'en-tête avec le nom du statut
        /// - Un FlowLayoutPanel défilable pour les cartes de tâches
        /// Active le AllowDrop pour accepter les tâches déposées depuis d'autres colonnes.
        /// </summary>
        /// <param name="statut">Le statut représenté par cette colonne.</param>
        /// <returns>Le contrôle Panel complet représentant la colonne.</returns>
        private Control CreateColumnControl(Statut statut)
        {
            // Panel de flux pour empiler les cartes verticalement avec défilement
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,      // Pas de retour à la ligne
                AutoScroll = true,         // Défilement vertical si trop de cartes
                BackColor = Color.FromArgb(240, 242, 245),
                Tag = statut,              // Stocke l'objet statut complet pour le drop
                Padding = new Padding(10),
                AllowDrop = true           // Autorise le DragDrop sur cette colonne
            };

            // ── Événements Drag-and-Drop ─────────────────────────────────────
            // DragEnter : accepte uniquement les objets de type Tache
            panel.DragEnter += (s, e) => {
                if (e.Data.GetDataPresent(typeof(Tache)))
                    e.Effect = DragDropEffects.Move;
            };

            // DragDrop : récupère la tâche et change son statut si différent
            panel.DragDrop += (s, e) => {
                var tache = (Tache)e.Data.GetData(typeof(Tache));
                var targetStatut = (Statut)((FlowLayoutPanel)s).Tag;

                // Ne change le statut que si la tâche est déposée dans une autre colonne
                if (tache.StatutId != targetStatut.Id)
                {
                    _controller.ChangerStatutTache(tache, targetStatut.Id);
                }
            };

            // ── En-tête de la colonne ─────────────────────────────────────────
            var header = new Label
            {
                Text = statut.Libelle.ToUpper(), // Texte en majuscules pour l'effet visuel
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Width = 200,
                Padding = new Padding(10),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(63, 81, 181),
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 40
            };

            // Assemblage : en-tête en haut, panel de cartes dessous
            var container = new Panel { Dock = DockStyle.Fill, Padding = new Padding(5) };
            container.Controls.Add(panel);
            container.Controls.Add(header);
            header.Dock = DockStyle.Top;
            panel.Dock = DockStyle.Fill;

            return container;
        }

        /// <summary>
        /// Vide toutes les colonnes puis redistribue les tâches dans la colonne
        /// correspondant à leur statut (<see cref="Tache.StatutId"/>).
        /// Appelé à chaque rafraîchissement du Kanban.
        /// </summary>
        /// <param name="taches">La liste complète des tâches du projet.</param>
        public void AfficherTaches(List<Tache> taches)
        {
            // Vidage de toutes les colonnes (FlowLayoutPanel dans chaque container)
            foreach (Control container in pnlKanban.Controls)
            {
                var flow = (FlowLayoutPanel)container.Controls[0];
                flow.Controls.Clear();
            }

            // Distribution de chaque tâche dans la colonne dont le statut correspond
            foreach (var tache in taches)
            {
                var card = CreateTaskCard(tache);
                foreach (Control container in pnlKanban.Controls)
                {
                    var flow = (FlowLayoutPanel)container.Controls[0];
                    // Comparaison par Id du statut stocké dans le Tag du FlowLayoutPanel
                    if (((Statut)flow.Tag).Id == tache.StatutId)
                    {
                        flow.Controls.Add(card);
                        break; // Passe à la tâche suivante dès que la colonne est trouvée
                    }
                }
            }
        }

        /// <summary>
        /// Crée le contrôle visuel d'une carte de tâche (task card).
        /// La carte affiche :
        /// - Une barre colorée à gauche indiquant la priorité
        /// - Le titre de la tâche
        /// - L'assigné (ou "Non assigné")
        /// - Un bouton "×" pour supprimer la tâche
        /// La carte supporte :
        /// - Le drag-and-drop (déclenché après un déplacement > 5px)
        /// - Le clic pour ouvrir les détails/modification
        /// </summary>
        /// <param name="tache">La tâche à représenter comme carte.</param>
        /// <returns>Le contrôle Panel représentant la carte.</returns>
        private Control CreateTaskCard(Tache tache)
        {
            var card = new Panel
            {
                Width = 220,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15), // Espacement sous chaque carte
                Padding = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Dessin d'une bordure plate grise autour de la carte
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.LightGray, ButtonBorderStyle.Solid);
            };

            // ── Barre de couleur de priorité (côté gauche) ───────────────────
            // Utilise la couleur hexadécimale de la priorité de la tâche,
            // ou gris par défaut si aucune priorité n'est assignée
            var colorBar = new Panel
            {
                Width = 5,
                Dock = DockStyle.Left,
                BackColor = string.IsNullOrEmpty(tache.Priorite?.CouleurHex)
                            ? Color.Gray
                            : ColorTranslator.FromHtml(tache.Priorite.CouleurHex)
            };
            card.Controls.Add(colorBar);

            // ── Titre de la tâche ─────────────────────────────────────────────
            var lblTitre = new Label
            {
                Text = tache.Titre,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 10),
                Width = 190,
                Height = 40,
                AutoEllipsis = true // Troncature avec "..." si le titre est trop long
            };
            card.Controls.Add(lblTitre);

            // ── Nom de l'assigné ──────────────────────────────────────────────
            var lblUser = new Label
            {
                Text = tache.Assignee?.Nom ?? "Non assigné", // Null-coalescing si non assigné
                Font = new Font("Segoe UI", 8),
                Location = new Point(15, 80),
                Width = 130,
                ForeColor = Color.DimGray
            };
            card.Controls.Add(lblUser);

            // ── Bouton de suppression rapide "×" ──────────────────────────────
            var btnDeleteTask = new Button
            {
                Text = "×",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(195, 2), // Coin supérieur droit de la carte
                Width = 22,
                Height = 22,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnDeleteTask.FlatAppearance.BorderSize = 0;
            // Clic sur "×" → délègue la suppression au contrôleur
            btnDeleteTask.Click += (s, e) => {
                _controller.SupprimerTache(tache);
            };
            card.Controls.Add(btnDeleteTask);
            // Mise au premier plan pour éviter que le clic soit intercepté par le Panel
            btnDeleteTask.BringToFront();

            // ── Support du Drag-and-Drop ──────────────────────────────────────
            // Le drag démarre seulement si la souris se déplace de plus de 5px
            // (seuil pour distinguer un clic d'un drag)
            Point dragStartPoint = Point.Empty;
            card.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Left)
                    dragStartPoint = e.Location;
            };

            card.MouseMove += (s, e) => {
                if (e.Button == MouseButtons.Left && dragStartPoint != Point.Empty)
                {
                    int dragThreshold = 5; // Seuil de déplacement en pixels
                    if (Math.Abs(e.X - dragStartPoint.X) > dragThreshold ||
                        Math.Abs(e.Y - dragStartPoint.Y) > dragThreshold)
                    {
                        // Lance l'opération de drag avec l'objet Tache comme donnée
                        card.DoDragDrop(tache, DragDropEffects.Move);
                        dragStartPoint = Point.Empty;
                    }
                }
            };

            card.MouseUp += (s, e) => dragStartPoint = Point.Empty;

            // ── Clic simple → Détails / Modification de la tâche ─────────────
            // L'action est définie une fois et appliquée à tous les éléments cliquables
            Action openDetails = () => _controller.ModifierTache(tache);

            card.Click += (s, e) => openDetails();
            lblTitre.Click += (s, e) => openDetails();
            lblUser.Click += (s, e) => openDetails();
            colorBar.Click += (s, e) => openDetails();

            return card;
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "Ajouter une tâche".
        /// Délègue la création d'une nouvelle tâche au contrôleur.
        /// </summary>
        private void btnAjouterTache_Click(object sender, EventArgs e)
        {
            _controller.AjouterTache();
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "Retour".
        /// Retourne à la liste des projets via le contrôleur.
        /// </summary>
        private void btnRetour_Click(object sender, EventArgs e)
        {
            _controller.RetourProjets();
        }
    }
}
