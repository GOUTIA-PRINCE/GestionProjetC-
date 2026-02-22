using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    public partial class UtilisateurForm : Form
    {
        private UtilisateurController _controller;
        private Utilisateur _utilisateurSelectionne;
        private List<Utilisateur> _utilisateurs;

        public UtilisateurForm()
        {
            InitializeComponent();
            ConfigurationDataGridView();
            ViderChamps();
        }

        public void SetController(UtilisateurController controller)
        {
            _controller = controller;
        }

        private void ConfigurationDataGridView()
        {
            dgvUtilisateurs.AutoGenerateColumns = false;

            dgvUtilisateurs.Columns.Clear();

            // Colonne ID
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50
            });

            // Colonne Nom
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "Nom",
                Width = 150
            });

            // Colonne Email
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 200
            });

            // Colonne Date de création
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateCreation",
                HeaderText = "Date création",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm"
                }
            });

            // Colonne Statut
            dgvUtilisateurs.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "EstActif",
                HeaderText = "Actif",
                Width = 50
            });
        }

        public void AfficherUtilisateurs(List<Utilisateur> utilisateurs)
        {
            _utilisateurs = utilisateurs;
            dgvUtilisateurs.DataSource = null;
            dgvUtilisateurs.DataSource = utilisateurs;
        }

        private void dgvUtilisateurs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUtilisateurs.SelectedRows.Count > 0)
            {
                var selectedRow = dgvUtilisateurs.SelectedRows[0];
                if (selectedRow.DataBoundItem is Utilisateur utilisateur)
                {
                    _utilisateurSelectionne = utilisateur;
                    AfficherUtilisateurDansFormulaire(utilisateur);
                }
            }
        }

        private void AfficherUtilisateurDansFormulaire(Utilisateur utilisateur)
        {
            txtNom.Text = utilisateur.Nom;
            txtEmail.Text = utilisateur.Email;
            txtMotDePasse.Text = ""; // Ne pas afficher le mot de passe
            chkEstActif.Checked = utilisateur.EstActif;
        }

        private void ViderChamps()
        {
            txtNom.Clear();
            txtEmail.Clear();
            txtMotDePasse.Clear();
            chkEstActif.Checked = true;
            _utilisateurSelectionne = null;
        }

        private Utilisateur GetUtilisateurFromForm()
        {
            return new Utilisateur
            {
                Id = _utilisateurSelectionne?.Id ?? 0,
                Nom = txtNom.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                MotDePasse = txtMotDePasse.Text,
                EstActif = chkEstActif.Checked
            };
        }

        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNom.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("L'email est obligatoire.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validation simple de l'email
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("L'email n'est pas valide.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Pour un nouvel utilisateur, le mot de passe est obligatoire
            if (_utilisateurSelectionne == null && string.IsNullOrWhiteSpace(txtMotDePasse.Text))
            {
                MessageBox.Show("Le mot de passe est obligatoire pour un nouvel utilisateur.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotDePasse.Focus();
                return false;
            }

            return true;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps()) return;

            var nouvelUtilisateur = GetUtilisateurFromForm();
            _controller.AjouterUtilisateur(nouvelUtilisateur);
            ViderChamps();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (_utilisateurSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à modifier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValiderChamps()) return;

            var utilisateurModifie = GetUtilisateurFromForm();
            _controller.ModifierUtilisateur(utilisateurModifie);
            ViderChamps();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_utilisateurSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _controller.SupprimerUtilisateur(_utilisateurSelectionne.Id);
            ViderChamps();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            ViderChamps();
            dgvUtilisateurs.ClearSelection();
        }

        private void btnActualiser_Click(object sender, EventArgs e)
        {
            _controller.ChargerUtilisateurs();
            ViderChamps();
        }
    }
}