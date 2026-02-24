using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class AnalyseForm : Form
    {
        public AnalyseForm()
        {
            InitializeComponent();
        }

        public void AfficherStatistiques(List<Projet> projets, List<Tache> taches, List<Statut> statuts)
        {
            flowLayoutPanel.Controls.Clear();

            // 1. Vue d'ensemble (Cards)
            AddStatCard("Projets Totaux", projets.Count.ToString(), Color.FromArgb(63, 81, 181));
            AddStatCard("Tâches Totales", taches.Count.ToString(), Color.FromArgb(0, 150, 136));
            
            int tachesTerminees = taches.Count(t => t.Statut?.Libelle?.ToLower() == "terminé" || t.StatutId == statuts.LastOrDefault()?.Id);
            double progression = taches.Count > 0 ? (double)tachesTerminees / taches.Count * 100 : 0;
            AddStatCard("Progression Globale", $"{progression:F0}%", Color.FromArgb(255, 152, 0));

            // 2. Répartition par Statut (Simple Bar Chart Custom)
            AddChartSection("Répartition des Tâches par Statut", taches, statuts);
        }

        private void AddStatCard(string title, string value, Color color)
        {
            Panel card = new Panel
            {
                Size = new Size(280, 120),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Dock = DockStyle.Top,
                Height = 30
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = color,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft
            };

            card.Paint += (s, e) => {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.FromArgb(240, 240, 240), ButtonBorderStyle.Solid);
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            flowLayoutPanel.Controls.Add(card);
        }

        private void AddChartSection(string title, List<Tache> taches, List<Statut> statuts)
        {
            Panel section = new Panel
            {
                Size = new Size(900, 400),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(20)
            };

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

            int maxCount = 0;
            var distribution = new Dictionary<string, int>();
            foreach (var statut in statuts)
            {
                int count = taches.Count(t => t.StatutId == statut.Id);
                distribution[statut.Libelle] = count;
                if (count > maxCount) maxCount = count;
            }

            int x = 50;
            int barWidth = 60;
            int spacing = 100;
            int maxHeight = 250;

            chartArea.Paint += (s, e) => {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int i = 0;
                foreach (var item in distribution)
                {
                    int h = maxCount > 0 ? (int)((double)item.Value / maxCount * maxHeight) : 0;
                    Rectangle barRect = new Rectangle(x + (i * spacing), chartArea.Height - h - 40, barWidth, h);
                    
                    // Shadow or subtle gradient would be nice, let's just do a nice color
                    using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(barRect, Color.FromArgb(63, 81, 181), Color.FromArgb(103, 119, 239), 90))
                    {
                        g.FillRectangle(brush, barRect);
                    }

                    // Label count
                    g.DrawString(item.Value.ToString(), new Font("Segoe UI", 9, FontStyle.Bold), Brushes.Black, x + (i * spacing) + (barWidth/4), chartArea.Height - h - 60);

                    // Label Statut
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center };
                    g.DrawString(item.Key, new Font("Segoe UI", 8), Brushes.DimGray, new Rectangle(x + (i * spacing) - 20, chartArea.Height - 35, barWidth + 40, 30), sf);

                    i++;
                }
            };

            section.Controls.Add(chartArea);
            section.Controls.Add(lblHeader);
            flowLayoutPanel.Controls.Add(section);
        }
    }
}
