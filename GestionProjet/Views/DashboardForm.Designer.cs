 namespace GestionProjet.Views
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblProjets;
        private System.Windows.Forms.Label lblTaches;

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
            this.lblProjets = new System.Windows.Forms.Label();
            this.lblTaches = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(30, 30);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(225, 65);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Tableau de Bord";
            // 
            // lblProjets
            // 
            this.lblProjets.AutoSize = true;
            this.lblProjets.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblProjets.Location = new System.Drawing.Point(40, 150);
            this.lblProjets.Name = "lblProjets";
            this.lblProjets.Size = new System.Drawing.Size(200, 38);
            this.lblProjets.TabIndex = 1;
            this.lblProjets.Text = "Projets Actifs : 0";
            // 
            // lblTaches
            // 
            this.lblTaches.AutoSize = true;
            this.lblTaches.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTaches.Location = new System.Drawing.Point(40, 220);
            this.lblTaches.Name = "lblTaches";
            this.lblTaches.Size = new System.Drawing.Size(188, 38);
            this.lblTaches.TabIndex = 2;
            this.lblTaches.Text = "Mes Tâches : 0";
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTaches);
            this.Controls.Add(this.lblProjets);
            this.Controls.Add(this.lblTitre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
