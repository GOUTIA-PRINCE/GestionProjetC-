namespace GestionProjet.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProjets;
        private System.Windows.Forms.Button btnTaches;
        private System.Windows.Forms.Button btnUtilisateurs;
        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.Button btnDeconnexion;
        private System.Windows.Forms.Label lblBienvenue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.LinkLabel lnkDeconnexion;

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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnDeconnexion = new System.Windows.Forms.Button();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.btnUtilisateurs = new System.Windows.Forms.Button();
            this.btnTaches = new System.Windows.Forms.Button();
            this.btnProjets = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lblBienvenue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lnkDeconnexion = new System.Windows.Forms.LinkLabel();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.pnlSidebar.Controls.Add(this.btnDeconnexion);
            this.pnlSidebar.Controls.Add(this.btnAnalyse);
            this.pnlSidebar.Controls.Add(this.btnUtilisateurs);
            this.pnlSidebar.Controls.Add(this.btnTaches);
            this.pnlSidebar.Controls.Add(this.btnProjets);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.lblLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(250, 720);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnDeconnexion
            // 
            this.btnDeconnexion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDeconnexion.FlatAppearance.BorderSize = 0;
            this.btnDeconnexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeconnexion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDeconnexion.ForeColor = System.Drawing.Color.White;
            this.btnDeconnexion.Location = new System.Drawing.Point(0, 670);
            this.btnDeconnexion.Name = "btnDeconnexion";
            this.btnDeconnexion.Size = new System.Drawing.Size(250, 50);
            this.btnDeconnexion.TabIndex = 5;
            this.btnDeconnexion.Text = "  Déconnexion";
            this.btnDeconnexion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeconnexion.UseVisualStyleBackColor = true;
            this.btnDeconnexion.Click += new System.EventHandler(this.menuDeconnexion_Click);
            // 
            // btnUtilisateurs
            // 
            this.btnUtilisateurs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUtilisateurs.FlatAppearance.BorderSize = 0;
            this.btnUtilisateurs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUtilisateurs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUtilisateurs.ForeColor = System.Drawing.Color.White;
            this.btnUtilisateurs.Location = new System.Drawing.Point(0, 230);
            this.btnUtilisateurs.Name = "btnUtilisateurs";
            this.btnUtilisateurs.Size = new System.Drawing.Size(250, 50);
            this.btnUtilisateurs.TabIndex = 4;
            this.btnUtilisateurs.Text = "  Utilisateurs";
            this.btnUtilisateurs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUtilisateurs.UseVisualStyleBackColor = true;
            this.btnUtilisateurs.Click += new System.EventHandler(this.menuUtilisateurs_Click);
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAnalyse.FlatAppearance.BorderSize = 0;
            this.btnAnalyse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyse.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAnalyse.ForeColor = System.Drawing.Color.White;
            this.btnAnalyse.Location = new System.Drawing.Point(0, 280);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(250, 50);
            this.btnAnalyse.TabIndex = 6;
            this.btnAnalyse.Text = "  Analyse";
            this.btnAnalyse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.menuAnalyse_Click);
            // 
            // btnTaches
            // 
            this.btnTaches.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTaches.FlatAppearance.BorderSize = 0;
            this.btnTaches.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaches.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTaches.ForeColor = System.Drawing.Color.White;
            this.btnTaches.Location = new System.Drawing.Point(0, 180);
            this.btnTaches.Name = "btnTaches";
            this.btnTaches.Size = new System.Drawing.Size(250, 50);
            this.btnTaches.TabIndex = 3;
            this.btnTaches.Text = "  Mes Tâches";
            this.btnTaches.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaches.UseVisualStyleBackColor = true;
            this.btnTaches.Click += new System.EventHandler(this.menuMesTaches_Click);
            // 
            // btnProjets
            // 
            this.btnProjets.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProjets.FlatAppearance.BorderSize = 0;
            this.btnProjets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProjets.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProjets.ForeColor = System.Drawing.Color.White;
            this.btnProjets.Location = new System.Drawing.Point(0, 130);
            this.btnProjets.Name = "btnProjets";
            this.btnProjets.Size = new System.Drawing.Size(250, 50);
            this.btnProjets.TabIndex = 2;
            this.btnProjets.Text = "  Projets";
            this.btnProjets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProjets.UseVisualStyleBackColor = true;
            this.btnProjets.Click += new System.EventHandler(this.menuProjet_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 80);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(250, 50);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "  Tableau de Bord";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.menuDashboard_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(250, 80);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "GestionProjet";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnMinimize);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.pnlUser);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(250, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(950, 80);
            this.pnlHeader.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(910, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += (s, e) => System.Windows.Forms.Application.Exit();
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.Location = new System.Drawing.Point(870, 0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(40, 30);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.Text = "-";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += (s, e) => this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.lblBienvenue);
            this.pnlUser.Controls.Add(this.lblStatus);
            this.pnlUser.Controls.Add(this.lnkDeconnexion);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUser.Location = new System.Drawing.Point(610, 0);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(300, 80);
            this.pnlUser.TabIndex = 2;
            // 
            // lnkDeconnexion
            // 
            this.lnkDeconnexion.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.lnkDeconnexion.AutoSize = true;
            this.lnkDeconnexion.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lnkDeconnexion.LinkColor = System.Drawing.Color.Red;
            this.lnkDeconnexion.Location = new System.Drawing.Point(215, 60);
            this.lnkDeconnexion.Name = "lnkDeconnexion";
            this.lnkDeconnexion.Size = new System.Drawing.Size(76, 19);
            this.lnkDeconnexion.TabIndex = 2;
            this.lnkDeconnexion.TabStop = true;
            this.lnkDeconnexion.Text = "Se déconnecter";
            this.lnkDeconnexion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkDeconnexion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDeconnexion_LinkClicked);
            // 
            // lblBienvenue
            // 
            this.lblBienvenue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBienvenue.Location = new System.Drawing.Point(10, 20);
            this.lblBienvenue.Name = "lblBienvenue";
            this.lblBienvenue.Size = new System.Drawing.Size(280, 25);
            this.lblBienvenue.TabIndex = 0;
            this.lblBienvenue.Text = "Bienvenue";
            this.lblBienvenue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(10, 45);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(280, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(250, 80);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(950, 640);
            this.pnlMainContent.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1200, 720);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de Projets";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlUser.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}