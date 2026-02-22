namespace GestionProjet.Views
{
    partial class UtilisateurForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvUtilisateurs;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblMotDePasse;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.CheckBox chkEstActif;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnActualiser;

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
            this.dgvUtilisateurs = new System.Windows.Forms.DataGridView();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.btnActualiser = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.chkEstActif = new System.Windows.Forms.CheckBox();
            this.txtMotDePasse = new System.Windows.Forms.TextBox();
            this.lblMotDePasse = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilisateurs)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.SuspendLayout();

            // dgvUtilisateurs
            this.dgvUtilisateurs.AllowUserToAddRows = false;
            this.dgvUtilisateurs.AllowUserToDeleteRows = false;
            this.dgvUtilisateurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUtilisateurs.Location = new System.Drawing.Point(12, 12);
            this.dgvUtilisateurs.MultiSelect = false;
            this.dgvUtilisateurs.Name = "dgvUtilisateurs";
            this.dgvUtilisateurs.ReadOnly = true;
            this.dgvUtilisateurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUtilisateurs.Size = new System.Drawing.Size(560, 250);
            this.dgvUtilisateurs.TabIndex = 0;
            this.dgvUtilisateurs.SelectionChanged += new System.EventHandler(this.dgvUtilisateurs_SelectionChanged);

            // gbDetails
            this.gbDetails.Controls.Add(this.btnActualiser);
            this.gbDetails.Controls.Add(this.btnAnnuler);
            this.gbDetails.Controls.Add(this.btnSupprimer);
            this.gbDetails.Controls.Add(this.btnModifier);
            this.gbDetails.Controls.Add(this.btnAjouter);
            this.gbDetails.Controls.Add(this.chkEstActif);
            this.gbDetails.Controls.Add(this.txtMotDePasse);
            this.gbDetails.Controls.Add(this.lblMotDePasse);
            this.gbDetails.Controls.Add(this.txtEmail);
            this.gbDetails.Controls.Add(this.lblEmail);
            this.gbDetails.Controls.Add(this.txtNom);
            this.gbDetails.Controls.Add(this.lblNom);
            this.gbDetails.Location = new System.Drawing.Point(12, 268);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(560, 170);
            this.gbDetails.TabIndex = 1;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Détails de l'utilisateur";

            // lblNom
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(20, 30);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(32, 13);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Nom:";

            // txtNom
            this.txtNom.Location = new System.Drawing.Point(100, 27);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(200, 20);
            this.txtNom.TabIndex = 1;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 60);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(100, 57);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 3;

            // lblMotDePasse
            this.lblMotDePasse.AutoSize = true;
            this.lblMotDePasse.Location = new System.Drawing.Point(20, 90);
            this.lblMotDePasse.Name = "lblMotDePasse";
            this.lblMotDePasse.Size = new System.Drawing.Size(74, 13);
            this.lblMotDePasse.TabIndex = 4;
            this.lblMotDePasse.Text = "Mot de passe:";

            // txtMotDePasse
            this.txtMotDePasse.Location = new System.Drawing.Point(100, 87);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.Size = new System.Drawing.Size(200, 20);
            this.txtMotDePasse.TabIndex = 5;
            this.txtMotDePasse.UseSystemPasswordChar = true;

            // chkEstActif
            this.chkEstActif.AutoSize = true;
            this.chkEstActif.Checked = true;
            this.chkEstActif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstActif.Location = new System.Drawing.Point(100, 120);
            this.chkEstActif.Name = "chkEstActif";
            this.chkEstActif.Size = new System.Drawing.Size(62, 17);
            this.chkEstActif.TabIndex = 6;
            this.chkEstActif.Text = "Est actif";
            this.chkEstActif.UseVisualStyleBackColor = true;

            // btnAjouter
            this.btnAjouter.Location = new System.Drawing.Point(350, 25);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(90, 30);
            this.btnAjouter.TabIndex = 7;
            this.btnAjouter.Text = "Ajouter";
            this.btnAjouter.UseVisualStyleBackColor = true;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);

            // btnModifier
            this.btnModifier.Location = new System.Drawing.Point(350, 60);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(90, 30);
            this.btnModifier.TabIndex = 8;
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = true;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);

            // btnSupprimer
            this.btnSupprimer.Location = new System.Drawing.Point(350, 95);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(90, 30);
            this.btnSupprimer.TabIndex = 9;
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.UseVisualStyleBackColor = true;
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);

            // btnAnnuler
            this.btnAnnuler.Location = new System.Drawing.Point(460, 25);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(90, 30);
            this.btnAnnuler.TabIndex = 10;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);

            // btnActualiser
            this.btnActualiser.Location = new System.Drawing.Point(460, 60);
            this.btnActualiser.Name = "btnActualiser";
            this.btnActualiser.Size = new System.Drawing.Size(90, 30);
            this.btnActualiser.TabIndex = 11;
            this.btnActualiser.Text = "Actualiser";
            this.btnActualiser.UseVisualStyleBackColor = true;
            this.btnActualiser.Click += new System.EventHandler(this.btnActualiser_Click);

            // UtilisateurForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.gbDetails);
            this.Controls.Add(this.dgvUtilisateurs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UtilisateurForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des utilisateurs";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilisateurs)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}