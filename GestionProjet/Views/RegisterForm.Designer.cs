
namespace GestionProjet.Views
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.TextBox txtConfirmerMotDePasse;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblMotDePasse;
        private System.Windows.Forms.Label lblConfirmerMotDePasse;
        private System.Windows.Forms.Button btnSInscrire;
        private System.Windows.Forms.Button btnRetour;
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
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMotDePasse = new System.Windows.Forms.TextBox();
            this.txtConfirmerMotDePasse = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblMotDePasse = new System.Windows.Forms.Label();
            this.lblConfirmerMotDePasse = new System.Windows.Forms.Label();
            this.btnSInscrire = new System.Windows.Forms.Button();
            this.btnRetour = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(100, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(180, 24);
            this.lblTitre.Text = "Créer un compte";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblNom
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(50, 70);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(32, 13);
            this.lblNom.Text = "Nom:";

            // txtNom
            this.txtNom.Location = new System.Drawing.Point(160, 67);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(180, 20);

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(50, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(160, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(180, 20);

            // lblMotDePasse
            this.lblMotDePasse.AutoSize = true;
            this.lblMotDePasse.Location = new System.Drawing.Point(50, 150);
            this.lblMotDePasse.Name = "lblMotDePasse";
            this.lblMotDePasse.Size = new System.Drawing.Size(74, 13);
            this.lblMotDePasse.Text = "Mot de passe:";

            // txtMotDePasse
            this.txtMotDePasse.Location = new System.Drawing.Point(160, 147);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.Size = new System.Drawing.Size(180, 20);
            this.txtMotDePasse.UseSystemPasswordChar = true;

            // lblConfirmerMotDePasse
            this.lblConfirmerMotDePasse.AutoSize = true;
            this.lblConfirmerMotDePasse.Location = new System.Drawing.Point(50, 190);
            this.lblConfirmerMotDePasse.Name = "lblConfirmerMotDePasse";
            this.lblConfirmerMotDePasse.Size = new System.Drawing.Size(100, 13);
            this.lblConfirmerMotDePasse.Text = "Confirmer:";

            // txtConfirmerMotDePasse
            this.txtConfirmerMotDePasse.Location = new System.Drawing.Point(160, 187);
            this.txtConfirmerMotDePasse.Name = "txtConfirmerMotDePasse";
            this.txtConfirmerMotDePasse.Size = new System.Drawing.Size(180, 20);
            this.txtConfirmerMotDePasse.UseSystemPasswordChar = true;

            // btnSInscrire
            this.btnSInscrire.Location = new System.Drawing.Point(100, 230);
            this.btnSInscrire.Name = "btnSInscrire";
            this.btnSInscrire.Size = new System.Drawing.Size(90, 30);
            this.btnSInscrire.Text = "S'inscrire";
            this.btnSInscrire.Click += new System.EventHandler(this.btnSInscrire_Click);

            // btnRetour
            this.btnRetour.Location = new System.Drawing.Point(200, 230);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(90, 30);
            this.btnRetour.Text = "Retour";
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);

            // RegisterForm
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblMotDePasse);
            this.Controls.Add(this.txtMotDePasse);
            this.Controls.Add(this.lblConfirmerMotDePasse);
            this.Controls.Add(this.txtConfirmerMotDePasse);
            this.Controls.Add(this.btnSInscrire);
            this.Controls.Add(this.btnRetour);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inscription - GestionProjet";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
