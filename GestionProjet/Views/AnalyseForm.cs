// =============================================================================
// Vue - Formulaire d'analyse (AnalyseForm)
// Affiche des statistiques visuelles sur les projets et tâches de l'utilisateur :
//   - Cartes de chiffres clés : nombre de projets, tâches, progression globale
//   - Graphique à barres (dessin custom via GDI+) : répartition des tâches par statut
// Toutes les données sont fournies par le contrôleur AnalyseController.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire d'analyse statistique de l'application.
    /// Affiche les métriques clés et un graphique à barres personnalisé dessiné
    /// via GDI+ (Paint event), sans dépendance à une bibliothèque de graphiques.
    /// </summary>
    public partial class AnalyseForm : Form
    {
        /// <summary>
        /// Initialise le formulaire d'analyse.
        /// </summary>
        public AnalyseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Point d'entrée principal pour l'affichage des statistiques.
        /// Vide d'abord le FlowLayoutPanel, puis crée et ajoute :
        /// 1. Des cartes de chiffres clés (projets totaux, tâches, progression)
        /// 2. Un graphique à barres de répartition des tâches par statut.
        /// </summary>
        /// <param name="projets">La liste des projets de l'utilisateur.</param>
        /// <param name="taches">Toutes les tâches de tous les projets (agrégées).</param>
        /// <param name="statuts">La liste des statuts disponibles (pour le graphique).</param>
        public void AfficherStatistiques(List<Projet> projets, List<Tache> taches, List<Statut> statuts)
        {
            flowLayoutPanel.Controls.Clear();

            // ── Cartes de chiffres clés ───────────────────────────────────────

            // Carte 1 : Nombre total de projets
            AddStatCard("Projets Totaux", projets.Count.ToString(), Color.FromArgb(63, 81, 181));

            // Carte 2 : Nombre total de tâches
            AddStatCard("Tâches Totales", taches.Count.ToString(), Color.FromArgb(0, 150, 136));

            // Carte 3 : Progression globale = (tâches terminées / total) * 100
            // Identifie les tâches terminées par le libellé "Terminé" ou l'id du dernier statut
            int tachesTerminees = taches.Count(t =>
                t.Statut?.Libelle?.ToLower() == "terminé" ||
                t.StatutId == statuts.LastOrDefault()?.Id);
            double progression = taches.Count > 0
                ? (double)tachesTerminees / taches.Count * 100
                : 0;
            AddStatCard("Progression Globale", $"{progression:F0}%", Color.FromArgb(255, 152, 0));

            // ── Graphique à barres de répartition des tâches par statut ───────
            AddChartSection("Répartition des Tâches par Statut", taches, statuts);
        }

        /// <summary>
        /// Crée et ajoute une carte statistique dans le FlowLayoutPanel.
        /// La carte affiche un titre en gris et une valeur chiffrée en grand texte coloré.
        /// Une bordure légère est dessinée via l'événement Paint.
        /// </summary>
        /// <param name="title">Le libellé de la statistique (ex : "Projets Totaux").</param>
        /// <param name="value">La valeur à afficher (ex : "42" ou "75%").</param>
        /// <param name="color">La couleur thématique de la valeur.</param>
        private void AddStatCard(string title, string value, Color color)
        {
            Panel card = new Panel
            {
                Size = new Size(280, 120),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            // Titre gris discret (partie supérieure de la carte)
            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Dock = DockStyle.Top,
                Height = 30
            };

            // Valeur en grand texte coloré (partie inférieure)
            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = color,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft
            };

            // Dessin d'une bordure gris clair autour de la carte
            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(240, 240, 240), ButtonBorderStyle.Solid);
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            flowLayoutPanel.Controls.Add(card);
        }

        /// <summary>
        /// Crée et ajoute une section graphique (graphique à barres vertical)
        /// dans le FlowLayoutPanel. Le graphique est dessiné entièrement via GDI+
        /// dans l'événement Paint du panel interne.
        /// Algorithme de dessin :
        ///   - Calcule la distribution des tâches par statut
        ///   - La hauteur de chaque barre est proportionnelle au max
        ///   - Un dégradé bleu est appliqué sur chaque barre
        ///   - Le nombre de tâches et le libellé du statut sont affichés sous/sur la barre
        /// </summary>
        /// <param name="title">Titre du graphique.</param>
        /// <param name="taches">Liste des tâches à analyser.</param>
        /// <param name="statuts">Liste des statuts définissant les barres du graphique.</param>
        private void AddChartSection(string title, List<Tache> taches, List<Statut> statuts)
        {
            Panel section = new Panel
            {
                Size = new Size(900, 400),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(20)
            };

            // Titre de la section graphique
            Label lblHeader = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40
            };

            Panel chartArea = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 20, 0, 0)
            };

            // ── Calcul de la distribution : nombre de tâches par statut ───────
            int maxCount = 0;
            var distribution = new Dictionary<string, int>();
            foreach (var statut in statuts)
            {
                int count = taches.Count(t => t.StatutId == statut.Id);
                distribution[statut.Libelle] = count;
                if (count > maxCount) maxCount = count; // Garde le maximum pour normaliser
            }

            // ── Paramètres de mise en page du graphique ───────────────────────
            int x = 50;         // Position X du début du graphique
            int barWidth = 60;  // Largeur de chaque barre
            int spacing = 100;  // Espacement entre barres (centre à centre)
            int maxHeight = 250; // Hauteur maximale des barres en pixels

            // ── Dessin GDI+ dans l'événement Paint ────────────────────────────
            chartArea.Paint += (s, e) => {
                var g = e.Graphics;
                // Anti-aliasing pour un rendu plus lisse
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int i = 0;
                foreach (var item in distribution)
                {
                    // Hauteur proportionnelle au maximum : h = (count / max) * maxHeight
                    int h = maxCount > 0 ? (int)((double)item.Value / maxCount * maxHeight) : 0;
                    Rectangle barRect = new Rectangle(
                        x + (i * spacing),
                        chartArea.Height - h - 40, // Ancrage en bas du panel
                        barWidth,
                        h);

                    // Dessin de la barre avec un dégradé bleu (bleu foncé → bleu clair)
                    if (h > 0)
                    {
                        using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                            barRect,
                            Color.FromArgb(63, 81, 181),   // Couleur du bas
                            Color.FromArgb(103, 119, 239), // Couleur du haut
                            90))                            // Angle vertical
                        {
                            g.FillRectangle(brush, barRect);
                        }
                    }

                    // Nombre de tâches au-dessus de la barre
                    g.DrawString(
                        item.Value.ToString(),
                        new Font("Segoe UI", 9, FontStyle.Bold),
                        Brushes.Black,
                        x + (i * spacing) + (barWidth / 4),
                        chartArea.Height - h - 60);

                    // Libellé du statut centré sous la barre
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };
                    g.DrawString(
                        item.Key,
                        new Font("Segoe UI", 8),
                        Brushes.DimGray,
                        new Rectangle(x + (i * spacing) - 20, chartArea.Height - 35, barWidth + 40, 30),
                        sf);

                    i++;
                }
            };

            section.Controls.Add(chartArea);
            section.Controls.Add(lblHeader);
            flowLayoutPanel.Controls.Add(section);
        }
    }
}
