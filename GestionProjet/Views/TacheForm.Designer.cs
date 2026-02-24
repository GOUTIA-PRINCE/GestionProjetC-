using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    partial class TacheForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTitre;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dtpEcheance;
        private System.Windows.Forms.ComboBox cmbStatut;
        private System.Windows.Forms.ComboBox cmbPriorite;
        private System.Windows.Forms.ComboBox cmbAssignee;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblEcheance;
        private System.Windows.Forms.Label lblStatut;
        private System.Windows.Forms.Label lblPriorite;
        private System.Windows.Forms.Label lblAssignee;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitre;

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
            this.txtTitre = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.dtpEcheance = new System.Windows.Forms.DateTimePicker();
            this.cmbStatut = new System.Windows.Forms.ComboBox();
            this.cmbPriorite = new System.Windows.Forms.ComboBox();
            this.cmbAssignee = new System.Windows.Forms.ComboBox();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblEcheance = new System.Windows.Forms.Label();
            this.lblStatut = new System.Windows.Forms.Label();
            this.lblPriorite = new System.Windows.Forms.Label();
            this.lblAssignee = new System.Windows.Forms.Label();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblFormTitre = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.pnlHeader.Controls.Add(this.lblFormTitre);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblFormTitre
            // 
            this.lblFormTitre.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFormTitre.ForeColor = System.Drawing.Color.White;
            this.lblFormTitre.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitre.Name = "lblFormTitre";
            this.lblFormTitre.Size = new System.Drawing.Size(500, 60);
            this.lblFormTitre.TabIndex = 0;
            this.lblFormTitre.Text = "Détails de la tâche";
            this.lblFormTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(30, 80);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(43, 25);
            this.lblTitre.TabIndex = 1;
            this.lblTitre.Text = "Titre";
            // 
            // txtTitre
            // 
            this.txtTitre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitre.Location = new System.Drawing.Point(30, 105);
            this.txtTitre.Name = "txtTitre";
            this.txtTitre.Size = new System.Drawing.Size(440, 34);
            this.txtTitre.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(30, 150);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(109, 25);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(30, 175);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(440, 100);
            this.txtDescription.TabIndex = 4;
            // 
            // lblStatut
            // 
            this.lblStatut.AutoSize = true;
            this.lblStatut.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatut.Location = new System.Drawing.Point(30, 290);
            this.lblStatut.Name = "lblStatut";
            this.lblStatut.Size = new System.Drawing.Size(63, 25);
            this.lblStatut.TabIndex = 5;
            this.lblStatut.Text = "Statut";
            // 
            // cmbStatut
            // 
            this.cmbStatut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatut.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatut.FormattingEnabled = true;
            this.cmbStatut.Location = new System.Drawing.Point(30, 315);
            this.cmbStatut.Name = "cmbStatut";
            this.cmbStatut.Size = new System.Drawing.Size(210, 36);
            this.cmbStatut.TabIndex = 6;
            // 
            // lblPriorite
            // 
            this.lblPriorite.AutoSize = true;
            this.lblPriorite.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPriorite.Location = new System.Drawing.Point(260, 290);
            this.lblPriorite.Name = "lblPriorite";
            this.lblPriorite.Size = new System.Drawing.Size(76, 25);
            this.lblPriorite.TabIndex = 7;
            this.lblPriorite.Text = "Priorité";
            // 
            // cmbPriorite
            // 
            this.cmbPriorite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriorite.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPriorite.FormattingEnabled = true;
            this.cmbPriorite.Location = new System.Drawing.Point(260, 315);
            this.cmbPriorite.Name = "cmbPriorite";
            this.cmbPriorite.Size = new System.Drawing.Size(210, 36);
            this.cmbPriorite.TabIndex = 8;
            // 
            // lblAssignee
            // 
            this.lblAssignee.AutoSize = true;
            this.lblAssignee.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAssignee.Location = new System.Drawing.Point(30, 370);
            this.lblAssignee.Name = "lblAssignee";
            this.lblAssignee.Size = new System.Drawing.Size(107, 25);
            this.lblAssignee.TabIndex = 9;
            this.lblAssignee.Text = "Responsable";
            // 
            // cmbAssignee
            // 
            this.cmbAssignee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAssignee.FormattingEnabled = true;
            this.cmbAssignee.Location = new System.Drawing.Point(30, 395);
            this.cmbAssignee.Name = "cmbAssignee";
            this.cmbAssignee.Size = new System.Drawing.Size(440, 36);
            this.cmbAssignee.TabIndex = 10;
            // 
            // lblEcheance
            // 
            this.lblEcheance.AutoSize = true;
            this.lblEcheance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEcheance.Location = new System.Drawing.Point(30, 450);
            this.lblEcheance.Name = "lblEcheance";
            this.lblEcheance.Size = new System.Drawing.Size(90, 25);
            this.lblEcheance.TabIndex = 11;
            this.lblEcheance.Text = "Échéance";
            // 
            // dtpEcheance
            // 
            this.dtpEcheance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEcheance.Location = new System.Drawing.Point(30, 475);
            this.dtpEcheance.Name = "dtpEcheance";
            this.dtpEcheance.Size = new System.Drawing.Size(440, 34);
            this.dtpEcheance.TabIndex = 12;
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnEnregistrer.FlatAppearance.BorderSize = 0;
            this.btnEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnregistrer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEnregistrer.ForeColor = System.Drawing.Color.White;
            this.btnEnregistrer.Location = new System.Drawing.Point(30, 550);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(210, 45);
            this.btnEnregistrer.TabIndex = 13;
            this.btnEnregistrer.Text = "ENREGISTRER";
            this.btnEnregistrer.UseVisualStyleBackColor = false;
            this.btnEnregistrer.Click += new System.EventHandler(this.btnEnregistrer_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnAnnuler.FlatAppearance.BorderSize = 0;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.btnAnnuler.Location = new System.Drawing.Point(260, 550);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(210, 45);
            this.btnAnnuler.TabIndex = 14;
            this.btnAnnuler.Text = "ANNULER";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // TacheForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 630);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnEnregistrer);
            this.Controls.Add(this.dtpEcheance);
            this.Controls.Add(this.lblEcheance);
            this.Controls.Add(this.cmbAssignee);
            this.Controls.Add(this.lblAssignee);
            this.Controls.Add(this.cmbPriorite);
            this.Controls.Add(this.lblPriorite);
            this.Controls.Add(this.cmbStatut);
            this.Controls.Add(this.lblStatut);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtTitre);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TacheForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tâche";
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
