 namespace GestionProjet.Views
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlProjets;
        private System.Windows.Forms.Label lblProjetsCount;
        private System.Windows.Forms.Label lblProjetsTitle;
        private System.Windows.Forms.Panel pnlTaches;
        private System.Windows.Forms.Label lblTachesCount;
        private System.Windows.Forms.Label lblTachesTitle;

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
            this.lblTitre = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlProjets = new System.Windows.Forms.Panel();
            this.lblProjetsCount = new System.Windows.Forms.Label();
            this.lblProjetsTitle = new System.Windows.Forms.Label();
            this.pnlTaches = new System.Windows.Forms.Panel();
            this.lblTachesCount = new System.Windows.Forms.Label();
            this.lblTachesTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlProjets.SuspendLayout();
            this.pnlTaches.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(30, 30);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(325, 54);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Tableau de Bord";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnlProjets);
            this.flowLayoutPanel1.Controls.Add(this.pnlTaches);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(39, 110);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 200);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // pnlProjets
            // 
            this.pnlProjets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.pnlProjets.Controls.Add(this.lblProjetsCount);
            this.pnlProjets.Controls.Add(this.lblProjetsTitle);
            this.pnlProjets.Location = new System.Drawing.Point(10, 10);
            this.pnlProjets.Margin = new System.Windows.Forms.Padding(10);
            this.pnlProjets.Name = "pnlProjets";
            this.pnlProjets.Size = new System.Drawing.Size(250, 120);
            this.pnlProjets.TabIndex = 0;
            // 
            // lblProjetsCount
            // 
            this.lblProjetsCount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblProjetsCount.ForeColor = System.Drawing.Color.White;
            this.lblProjetsCount.Location = new System.Drawing.Point(0, 45);
            this.lblProjetsCount.Name = "lblProjetsCount";
            this.lblProjetsCount.Size = new System.Drawing.Size(250, 45);
            this.lblProjetsCount.TabIndex = 1;
            this.lblProjetsCount.Text = "0";
            this.lblProjetsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProjetsTitle
            // 
            this.lblProjetsTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProjetsTitle.ForeColor = System.Drawing.Color.White;
            this.lblProjetsTitle.Location = new System.Drawing.Point(0, 15);
            this.lblProjetsTitle.Name = "lblProjetsTitle";
            this.lblProjetsTitle.Size = new System.Drawing.Size(250, 23);
            this.lblProjetsTitle.TabIndex = 0;
            this.lblProjetsTitle.Text = "Projets Actifs";
            this.lblProjetsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTaches
            // 
            this.pnlTaches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(129)))));
            this.pnlTaches.Controls.Add(this.lblTachesCount);
            this.pnlTaches.Controls.Add(this.lblTachesTitle);
            this.pnlTaches.Location = new System.Drawing.Point(280, 10);
            this.pnlTaches.Margin = new System.Windows.Forms.Padding(10);
            this.pnlTaches.Name = "pnlTaches";
            this.pnlTaches.Size = new System.Drawing.Size(250, 120);
            this.pnlTaches.TabIndex = 1;
            // 
            // lblTachesCount
            // 
            this.lblTachesCount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTachesCount.ForeColor = System.Drawing.Color.White;
            this.lblTachesCount.Location = new System.Drawing.Point(0, 45);
            this.lblTachesCount.Name = "lblTachesCount";
            this.lblTachesCount.Size = new System.Drawing.Size(250, 45);
            this.lblTachesCount.TabIndex = 1;
            this.lblTachesCount.Text = "0";
            this.lblTachesCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTachesTitle
            // 
            this.lblTachesTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTachesTitle.ForeColor = System.Drawing.Color.White;
            this.lblTachesTitle.Location = new System.Drawing.Point(0, 15);
            this.lblTachesTitle.Name = "lblTachesTitle";
            this.lblTachesTitle.Size = new System.Drawing.Size(250, 23);
            this.lblTachesTitle.TabIndex = 0;
            this.lblTachesTitle.Text = "Mes Tâches";
            this.lblTachesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblTitre);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardForm";
            this.Text = "Tableau de Bord";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlProjets.ResumeLayout(false);
            this.pnlTaches.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();


        }
    }
}
