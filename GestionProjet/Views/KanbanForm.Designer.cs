namespace GestionProjet.Views
{
    partial class KanbanForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel pnlKanban;
        private System.Windows.Forms.Label lblProjetTitre;
        private System.Windows.Forms.Button btnAjouterTache;
        private System.Windows.Forms.Button btnRetour;

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
            this.pnlKanban = new System.Windows.Forms.TableLayoutPanel();
            this.lblProjetTitre = new System.Windows.Forms.Label();
            this.btnAjouterTache = new System.Windows.Forms.Button();
            this.btnRetour = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblProjetTitre
            this.lblProjetTitre.AutoSize = true;
            this.lblProjetTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblProjetTitre.Location = new System.Drawing.Point(20, 20);
            this.lblProjetTitre.Name = "lblProjetTitre";
            this.lblProjetTitre.Size = new System.Drawing.Size(200, 26);
            this.lblProjetTitre.Text = "Kanban : Projet";

            this.btnAjouterTache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAjouterTache.Location = new System.Drawing.Point(750, 20);
            this.btnAjouterTache.Name = "btnAjouterTache";
            this.btnAjouterTache.Size = new System.Drawing.Size(120, 35);
            this.btnAjouterTache.Text = "+ Ajouter une tâche";
            this.btnAjouterTache.UseVisualStyleBackColor = true;
            this.btnAjouterTache.Click += new System.EventHandler(this.btnAjouterTache_Click);

            this.btnRetour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetour.Location = new System.Drawing.Point(880, 20);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(80, 35);
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = true;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);

            this.pnlKanban.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKanban.ColumnCount = 1;
            this.pnlKanban.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKanban.Location = new System.Drawing.Point(20, 70);
            this.pnlKanban.Name = "pnlKanban";
            this.pnlKanban.RowCount = 1;
            this.pnlKanban.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKanban.Size = new System.Drawing.Size(960, 500);

            // KanbanForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.btnRetour);
            this.Controls.Add(this.btnAjouterTache);
            this.Controls.Add(this.lblProjetTitre);
            this.Controls.Add(this.pnlKanban);
            this.Name = "KanbanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vue Kanban";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
