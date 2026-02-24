namespace GestionProjet.Views
{
    partial class ProjetForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvProjets;
        private System.Windows.Forms.Button btnNouveauProjet;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Panel pnlHeader;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjets)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProjets
            // 
            this.dgvProjets.AllowUserToAddRows = false;
            this.dgvProjets.AllowUserToDeleteRows = false;
            this.dgvProjets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProjets.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProjets.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProjets.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProjets.ColumnHeadersHeight = 40;
            this.dgvProjets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProjets.EnableHeadersVisualStyles = false;
            this.dgvProjets.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.dgvProjets.Location = new System.Drawing.Point(20, 100);
            this.dgvProjets.Name = "dgvProjets";
            this.dgvProjets.ReadOnly = true;
            this.dgvProjets.RowHeadersVisible = false;
            this.dgvProjets.RowHeadersWidth = 62;
            this.dgvProjets.RowTemplate.Height = 50;
            this.dgvProjets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjets.Size = new System.Drawing.Size(910, 500);
            this.dgvProjets.TabIndex = 0;
            this.dgvProjets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjets_CellContentClick);
            // 
            // btnNouveauProjet
            // 
            this.btnNouveauProjet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNouveauProjet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnNouveauProjet.FlatAppearance.BorderSize = 0;
            this.btnNouveauProjet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNouveauProjet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNouveauProjet.ForeColor = System.Drawing.Color.White;
            this.btnNouveauProjet.Location = new System.Drawing.Point(710, 20);
            this.btnNouveauProjet.Name = "btnNouveauProjet";
            this.btnNouveauProjet.Size = new System.Drawing.Size(180, 40);
            this.btnNouveauProjet.TabIndex = 1;
            this.btnNouveauProjet.Text = "+ Nouveau Projet";
            this.btnNouveauProjet.UseVisualStyleBackColor = false;
            this.btnNouveauProjet.Click += new System.EventHandler(this.btnNouveauProjet_Click);
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(0, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(148, 48);
            this.lblTitre.TabIndex = 2;
            this.lblTitre.Text = "Mes Projets";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitre);
            this.pnlHeader.Controls.Add(this.btnNouveauProjet);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(20, 20);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(910, 80);
            this.pnlHeader.TabIndex = 3;
            // 
            // ProjetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(950, 620);
            this.Controls.Add(this.dgvProjets);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProjetForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "Mes Projets";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjets)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
