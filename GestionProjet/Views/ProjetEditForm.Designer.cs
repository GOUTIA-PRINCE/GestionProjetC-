using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionProjet.Views
{
    partial class ProjetEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dtpDateFin;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDateFin;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitre;

        // ── Nouveaux contrôles pour la gestion des membres ──────────────────
        private System.Windows.Forms.Label lblTousUtilisateurs;
        private System.Windows.Forms.Label lblMembres;
        private System.Windows.Forms.ListBox lstTousUtilisateurs;
        private System.Windows.Forms.ListBox lstMembres;
        private System.Windows.Forms.Button btnAjouterMembre;
        private System.Windows.Forms.Button btnRetirerMembre;

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
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dtpDateFin = new System.Windows.Forms.DateTimePicker();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDateFin = new System.Windows.Forms.Label();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblTousUtilisateurs = new System.Windows.Forms.Label();
            this.lblMembres = new System.Windows.Forms.Label();
            this.lstTousUtilisateurs = new System.Windows.Forms.ListBox();
            this.lstMembres = new System.Windows.Forms.ListBox();
            this.btnAjouterMembre = new System.Windows.Forms.Button();
            this.btnRetirerMembre = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ───────────────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(63, 81, 181);
            this.pnlHeader.Controls.Add(this.lblTitre);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(700, 70);
            this.pnlHeader.TabIndex = 0;

            // ── lblTitre ────────────────────────────────────────────────────
            this.lblTitre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.White;
            this.lblTitre.Location = new System.Drawing.Point(0, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(700, 70);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Nouveau Projet";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── lblNom ──────────────────────────────────────────────────────
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNom.Location = new System.Drawing.Point(30, 90);
            this.lblNom.Name = "lblNom";
            this.lblNom.Text = "Nom du projet";

            // ── txtNom ──────────────────────────────────────────────────────
            this.txtNom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNom.Location = new System.Drawing.Point(30, 115);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(640, 34);
            this.txtNom.TabIndex = 1;

            // ── lblDescription ──────────────────────────────────────────────
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(30, 165);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Text = "Description";

            // ── txtDescription ──────────────────────────────────────────────
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(30, 190);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(640, 80);
            this.txtDescription.TabIndex = 2;

            // ── lblDateFin ──────────────────────────────────────────────────
            this.lblDateFin.AutoSize = true;
            this.lblDateFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDateFin.Location = new System.Drawing.Point(30, 285);
            this.lblDateFin.Name = "lblDateFin";
            this.lblDateFin.Text = "Date fin prévue";

            // ── dtpDateFin ──────────────────────────────────────────────────
            this.dtpDateFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDateFin.Location = new System.Drawing.Point(30, 310);
            this.dtpDateFin.Name = "dtpDateFin";
            this.dtpDateFin.Size = new System.Drawing.Size(640, 34);
            this.dtpDateFin.TabIndex = 3;

            // ── SECTION MEMBRES ──────────────────────────────────────────────

            // Label "Tous les utilisateurs"
            this.lblTousUtilisateurs.AutoSize = true;
            this.lblTousUtilisateurs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTousUtilisateurs.Location = new System.Drawing.Point(30, 365);
            this.lblTousUtilisateurs.Name = "lblTousUtilisateurs";
            this.lblTousUtilisateurs.Text = "Utilisateurs disponibles";

            // ListBox gauche : tous les utilisateurs
            this.lstTousUtilisateurs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstTousUtilisateurs.Location = new System.Drawing.Point(30, 390);
            this.lstTousUtilisateurs.Name = "lstTousUtilisateurs";
            this.lstTousUtilisateurs.Size = new System.Drawing.Size(270, 160);
            this.lstTousUtilisateurs.TabIndex = 4;
            this.lstTousUtilisateurs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTousUtilisateurs.DoubleClick += new System.EventHandler(this.lstTousUtilisateurs_DoubleClick);

            // Bouton >> Ajouter membre
            this.btnAjouterMembre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAjouterMembre.Text = "►";
            this.btnAjouterMembre.Location = new System.Drawing.Point(315, 430);
            this.btnAjouterMembre.Size = new System.Drawing.Size(50, 35);
            this.btnAjouterMembre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterMembre.BackColor = System.Drawing.Color.FromArgb(63, 81, 181);
            this.btnAjouterMembre.ForeColor = System.Drawing.Color.White;
            this.btnAjouterMembre.FlatAppearance.BorderSize = 0;
            this.btnAjouterMembre.TabIndex = 5;
            this.btnAjouterMembre.Click += new System.EventHandler(this.btnAjouterMembre_Click);

            // Bouton << Retirer membre
            this.btnRetirerMembre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRetirerMembre.Text = "◄";
            this.btnRetirerMembre.Location = new System.Drawing.Point(315, 480);
            this.btnRetirerMembre.Size = new System.Drawing.Size(50, 35);
            this.btnRetirerMembre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetirerMembre.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnRetirerMembre.ForeColor = System.Drawing.Color.White;
            this.btnRetirerMembre.FlatAppearance.BorderSize = 0;
            this.btnRetirerMembre.TabIndex = 6;
            this.btnRetirerMembre.Click += new System.EventHandler(this.btnRetirerMembre_Click);

            // Label "Membres du projet"
            this.lblMembres.AutoSize = true;
            this.lblMembres.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMembres.Location = new System.Drawing.Point(380, 365);
            this.lblMembres.Name = "lblMembres";
            this.lblMembres.Text = "Membres du projet";

            // ListBox droite : membres sélectionnés
            this.lstMembres.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstMembres.Location = new System.Drawing.Point(380, 390);
            this.lstMembres.Name = "lstMembres";
            this.lstMembres.Size = new System.Drawing.Size(290, 160);
            this.lstMembres.TabIndex = 7;
            this.lstMembres.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMembres.DoubleClick += new System.EventHandler(this.lstMembres_DoubleClick);

            // ── Boutons Enregistrer / Annuler ────────────────────────────────

            this.btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(63, 81, 181);
            this.btnEnregistrer.FlatAppearance.BorderSize = 0;
            this.btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistrer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEnregistrer.ForeColor = System.Drawing.Color.White;
            this.btnEnregistrer.Location = new System.Drawing.Point(30, 575);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(305, 45);
            this.btnEnregistrer.TabIndex = 8;
            this.btnEnregistrer.Text = "ENREGISTRER";
            this.btnEnregistrer.UseVisualStyleBackColor = false;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);

            this.btnAnnuler.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.btnAnnuler.FlatAppearance.BorderSize = 0;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnnuler.ForeColor = System.Drawing.Color.Black;
            this.btnAnnuler.Location = new System.Drawing.Point(365, 575);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(305, 45);
            this.btnAnnuler.TabIndex = 9;
            this.btnAnnuler.Text = "ANNULER";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);

            // ── ProjetEditForm (fenêtre principale) ──────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 645);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.lstMembres);
            this.Controls.Add(this.lblMembres);
            this.Controls.Add(this.btnRetirerMembre);
            this.Controls.Add(this.btnAjouterMembre);
            this.Controls.Add(this.lstTousUtilisateurs);
            this.Controls.Add(this.lblTousUtilisateurs);
            this.Controls.Add(this.dtpDateFin);
            this.Controls.Add(this.lblDateFin);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ProjetEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Détails du projet";
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
