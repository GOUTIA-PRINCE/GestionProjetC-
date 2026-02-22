namespace GestionProjet.Views
{
    partial class ProjetForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProjets;
        private System.Windows.Forms.Button btnNouveauProjet;
        private System.Windows.Forms.Label lblTitre;

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
            this.dgvProjets = new System.Windows.Forms.DataGridView();
            this.btnNouveauProjet = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjets)).BeginInit();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(20, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(180, 24);
            this.lblTitre.Text = "Mes Projets";

            // btnNouveauProjet
            this.btnNouveauProjet.Location = new System.Drawing.Point(650, 20);
            this.btnNouveauProjet.Name = "btnNouveauProjet";
            this.btnNouveauProjet.Size = new System.Drawing.Size(120, 30);
            this.btnNouveauProjet.Text = "+ Nouveau Projet";
            this.btnNouveauProjet.UseVisualStyleBackColor = true;
            this.btnNouveauProjet.Click += new System.EventHandler(this.btnNouveauProjet_Click);

            // dgvProjets
            this.dgvProjets.AllowUserToAddRows = false;
            this.dgvProjets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjets.Location = new System.Drawing.Point(20, 70);
            this.dgvProjets.Name = "dgvProjets";
            this.dgvProjets.ReadOnly = true;
            this.dgvProjets.Size = new System.Drawing.Size(750, 350);
            this.dgvProjets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjets_CellContentClick);

            // ProjetForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.btnNouveauProjet);
            this.Controls.Add(this.dgvProjets);
            this.Name = "ProjetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des Projets";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
