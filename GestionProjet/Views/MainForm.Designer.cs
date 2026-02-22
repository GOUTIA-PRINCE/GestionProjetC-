namespace GestionProjet.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFichier;
        private System.Windows.Forms.ToolStripMenuItem menuDashboard;
        private System.Windows.Forms.ToolStripMenuItem menuDeconnexion;
        private System.Windows.Forms.ToolStripMenuItem menuQuitter;
        private System.Windows.Forms.ToolStripMenuItem menuGestion;
        private System.Windows.Forms.ToolStripMenuItem menuUtilisateurs;
        private System.Windows.Forms.ToolStripMenuItem menuProjet;
        private System.Windows.Forms.ToolStripMenuItem menuCreerProjet;
        private System.Windows.Forms.ToolStripMenuItem menuTache;
        private System.Windows.Forms.ToolStripMenuItem menuMesTaches;
        private System.Windows.Forms.ToolStripMenuItem menuToutesTaches;
        private System.Windows.Forms.Label lblBienvenue;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlMainContent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFichier = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeconnexion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuitter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGestion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUtilisateurs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProjet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreerProjet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTache = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMesTaches = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToutesTaches = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblBienvenue = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFichier,
            this.menuDashboard,
            this.menuGestion,
            this.menuProjet,
            this.menuTache});
           this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 35);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuFichier
            // 
            this.menuFichier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDeconnexion,
            this.menuQuitter});
            this.menuFichier.Name = "menuFichier";
            this.menuFichier.Size = new System.Drawing.Size(78, 29);
            this.menuFichier.Text = "Fichier";
            // 
            // menuDashboard
            // 
            this.menuDashboard.Name = "menuDashboard";
            this.menuDashboard.Size = new System.Drawing.Size(160, 29);
            this.menuDashboard.Text = "Tableau de Bord";
            this.menuDashboard.Click += new System.EventHandler(this.menuDashboard_Click);
            // 
            // menuDeconnexion
            // 
            this.menuDeconnexion.Name = "menuDeconnexion";
            this.menuDeconnexion.Size = new System.Drawing.Size(217, 34);
            this.menuDeconnexion.Text = "Déconnexion";
            this.menuDeconnexion.Click += new System.EventHandler(this.menuDeconnexion_Click);
            // 
            // menuQuitter
            // 
            this.menuQuitter.Name = "menuQuitter";
            this.menuQuitter.Size = new System.Drawing.Size(217, 34);
            this.menuQuitter.Text = "Quitter";
            this.menuQuitter.Click += new System.EventHandler(this.menuQuitter_Click);
            // 
            // menuGestion
            // 
            this.menuGestion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUtilisateurs});
           this.menuGestion.Name = "menuGestion";
            this.menuGestion.Size = new System.Drawing.Size(88, 29);
            this.menuGestion.Text = "Gestion";
            // 
            // menuUtilisateurs
            // 
            this.menuUtilisateurs.Name = "menuUtilisateurs";
            this.menuUtilisateurs.Size = new System.Drawing.Size(200, 34);
            this.menuUtilisateurs.Text = "Utilisateurs";
            this.menuUtilisateurs.Click += new System.EventHandler(this.menuUtilisateurs_Click);
            // 
            // menuProjet
            // 
            this.menuProjet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCreerProjet});
            this.menuProjet.Name = "menuProjet";
            this.menuProjet.Size = new System.Drawing.Size(74, 29);
            this.menuProjet.Text = "Projet";
            this.menuProjet.Click += new System.EventHandler(this.menuProjet_Click);
            // 
            // menuCreerProjet
            // 
            this.menuCreerProjet.Name = "menuCreerProjet";
            this.menuCreerProjet.Size = new System.Drawing.Size(243, 34);
            this.menuCreerProjet.Text = "Créer un projet";
            this.menuCreerProjet.Click += new System.EventHandler(this.menuCreerProjet_Click);
            // 
            // menuTache
            // 
            this.menuTache.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMesTaches,
            this.menuToutesTaches});
            this.menuTache.Name = "menuTache";
            this.menuTache.Size = new System.Drawing.Size(70, 29);
            this.menuTache.Text = "Tâche";
            this.menuTache.Click += new System.EventHandler(this.menuTache_Click);
            // 
            // menuMesTaches
            // 
            this.menuMesTaches.Name = "menuMesTaches";
            this.menuMesTaches.Size = new System.Drawing.Size(270, 34);
            this.menuMesTaches.Text = "Mes tâches";
            this.menuMesTaches.Click += new System.EventHandler(this.menuMesTaches_Click);
            // 
            // menuToutesTaches
            // 
            this.menuToutesTaches.Name = "menuToutesTaches";
            this.menuToutesTaches.Size = new System.Drawing.Size(270, 34);
            this.menuToutesTaches.Text = "Toutes les tâches";
            this.menuToutesTaches.Click += new System.EventHandler(this.menuToutesTaches_Click);
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(0, 85);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1200, 575);
            this.pnlMainContent.TabIndex = 3;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlHeader.Controls.Add(this.lblBienvenue);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 35);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 50);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblBienvenue
            // 
            this.lblBienvenue.AutoSize = true;
            this.lblBienvenue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBienvenue.Location = new System.Drawing.Point(20, 10);
            this.lblBienvenue.Name = "lblBienvenue";
            this.lblBienvenue.Size = new System.Drawing.Size(111, 28);
            this.lblBienvenue.TabIndex = 1;
            this.lblBienvenue.Text = "Bienvenue";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 660);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip.Size = new System.Drawing.Size(1200, 32);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(258, 25);
            this.lblStatus.Text = "Connecté à la base de données";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GestionProjet - Accueil";
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}