// =============================================================================
// Vue - Gestion des utilisateurs (UtilisateurForm)
// Affiche la liste des utilisateurs dans un DataGridView avec un formulaire
// latéral pour créer, modifier ou supprimer un utilisateur.
// Gestion des droits basée sur le rôle de l'utilisateur connecté :
//   - Admin → peut tout faire (ajouter, modifier, supprimer)
//   - User  → peut uniquement modifier ses propres informations
// =============================================================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire de gestion des utilisateurs (liste + édition en ligne).
    /// Gère les droits d'accès aux boutons d'action selon le rôle de l'utilisateur courant.
    /// </summary>
    public partial class UtilisateurForm : Form
    {
        /// <summary>Référence vers le contrôleur utilisateur.</summary>
        private UtilisateurController _controller;

        /// <summary>L'utilisateur sélectionné dans le DataGridView, ou null si aucun.</summary>
        private Utilisateur _utilisateurSelectionne;

        /// <summary>
        /// Cache local de la liste des utilisateurs affichés dans la grille.
        /// Permet de récupérer l'objet utilisateur d'une ligne sélectionnée.
        /// </summary>
        private List<Utilisateur> _utilisateurs;

        /// <summary>L'utilisateur actuellement connecté (détermine les droits d'accès UI).</summary>
        private Utilisateur _utilisateurCourant;

        /// <summary>
        /// Initialise le formulaire, configure le DataGridView et vide le formulaire de saisie.
        /// </summary>
        public UtilisateurForm()
        {
            InitializeComponent();
            ConfigurationDataGridView();
            ViderChamps();
        }

        /// <summary>
        /// Enregistre le contrôleur associé à cette vue.
        /// </summary>
        /// <param name="controller">Le contrôleur de gestion des utilisateurs.</param>
        public void SetController(UtilisateurController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Injecte l'utilisateur courant et configure les boutons selon son rôle.
        /// Un User simple ne peut pas ajouter de nouveaux utilisateurs ni changer leur statut actif.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur connecté.</param>
        public void SetCurrentUser(Utilisateur utilisateur)
        {
            _utilisateurCourant = utilisateur;

            // Restriction d'interface pour les non-administrateurs
            if (_utilisateurCourant.Role != "Admin")
            {
                btnAjouter.Enabled = false;       // Masque le bouton Ajouter
                chkEstActif.Enabled = false;       // Empêche de changer le statut actif/inactif
            }
        }

        /// <summary>
        /// Configure les colonnes du DataGridView de manière programmatique :
        /// colonnes Nom, Email, Date de création (format court) et Actif (case à cocher).
        /// </summary>
        private void ConfigurationDataGridView()
        {
            dgvUtilisateurs.AutoGenerateColumns = false;
            dgvUtilisateurs.Columns.Clear();

            // Style des en-têtes : fond primaire bleu indigo, texte blanc
            dgvUtilisateurs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181);
            dgvUtilisateurs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUtilisateurs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Colonne Nom
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nom",
                HeaderText = "NOM",
                FillWeight = 30
            });

            // Colonne Email
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "EMAIL",
                FillWeight = 30
            });

            // Colonne Date de création (format "g" = date+heure courte)
            dgvUtilisateurs.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateCreation",
                HeaderText = "DATE",
                FillWeight = 25,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });

            // Colonne Actif (case à cocher, lecture seule dans la grille)
            dgvUtilisateurs.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "EstActif",
                HeaderText = "ACTIF",
                FillWeight = 15
            });
        }

        /// <summary>
        /// Met à jour la source de données du DataGridView avec la liste d'utilisateurs.
        /// La double assignation (null puis liste) force le rechargement complet.
        /// </summary>
        /// <param name="utilisateurs">La liste des utilisateurs à afficher.</param>
        public void AfficherUtilisateurs(List<Utilisateur> utilisateurs)
        {
            _utilisateurs = utilisateurs;
            dgvUtilisateurs.DataSource = null;
            dgvUtilisateurs.DataSource = utilisateurs;
        }

        /// <summary>
        /// Déclenché quand la sélection dans le DataGridView change.
        /// Pré-remplit le formulaire de saisie avec les données de l'utilisateur sélectionné
        /// et ajuste les boutons d'action selon les droits.
        /// </summary>
        private void dgvUtilisateurs_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUtilisateurs.SelectedRows.Count > 0)
            {
                var selectedRow = dgvUtilisateurs.SelectedRows[0];
                if (selectedRow.DataBoundItem is Utilisateur utilisateur)
                {
                    _utilisateurSelectionne = utilisateur;
                    AfficherUtilisateurDansFormulaire(utilisateur);

                    // Calcul des droits selon le rôle de l'utilisateur courant :
                    // Peut modifier : Admin (tous) OU l'utilisateur lui-même
                    bool peutModifier = _utilisateurCourant.Role == "Admin" ||
                                        _utilisateurCourant.Id == utilisateur.Id;
                    // Peut supprimer : Admin uniquement, mais pas lui-même
                    bool peutSupprimer = _utilisateurCourant.Role == "Admin" &&
                                        _utilisateurCourant.Id != utilisateur.Id;

                    btnModifier.Enabled = peutModifier;
                    btnSupprimer.Enabled = peutSupprimer;
                }
            }
            else
            {
                // Aucune ligne sélectionnée → réinitialisation des boutons
                ViderChamps();
                // Le bouton Ajouter est disponible seulement si l'utilisateur est Admin
                btnAjouter.Enabled = (_utilisateurCourant?.Role == "Admin");
                btnModifier.Enabled = false;
                btnSupprimer.Enabled = false;
            }
        }

        /// <summary>
        /// Remplit les champs du formulaire de saisie avec les données de l'utilisateur sélectionné.
        /// Le mot de passe n'est jamais affiché (sécurité).
        /// </summary>
        /// <param name="utilisateur">L'utilisateur dont on affiche les données.</param>
        private void AfficherUtilisateurDansFormulaire(Utilisateur utilisateur)
        {
            txtNom.Text = utilisateur.Nom;
            txtEmail.Text = utilisateur.Email;
            txtMotDePasse.Text = ""; // Ne jamais afficher le mot de passe (même haché)
            chkEstActif.Checked = utilisateur.EstActif;
        }

        /// <summary>
        /// Vide tous les champs du formulaire de saisie et désélectionne l'utilisateur courant.
        /// Appelé après une action réussie ou lors du clic sur "Annuler".
        /// </summary>
        private void ViderChamps()
        {
            txtNom.Clear();
            txtEmail.Clear();
            txtMotDePasse.Clear();
            chkEstActif.Checked = true;       // Actif par défaut pour un nouvel utilisateur
            _utilisateurSelectionne = null;   // Plus d'utilisateur sélectionné
        }

        /// <summary>
        /// Construit un objet <see cref="Utilisateur"/> à partir des données saisies dans le formulaire.
        /// L'Id est récupéré depuis l'utilisateur sélectionné (0 si c'est un nouvel utilisateur).
        /// </summary>
        /// <returns>Un utilisateur avec les données du formulaire.</returns>
        private Utilisateur GetUtilisateurFromForm()
        {
            return new Utilisateur
            {
                Id = _utilisateurSelectionne?.Id ?? 0, // 0 = nouvel utilisateur (non encore en base)
                Nom = txtNom.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                MotDePasse = txtMotDePasse.Text,        // En clair, sera haché par le repository
                EstActif = chkEstActif.Checked
            };
        }

        /// <summary>
        /// Valide les champs du formulaire avant d'appeler le contrôleur.
        /// Règles : Nom obligatoire, Email obligatoire et format valide,
        /// Mot de passe obligatoire uniquement pour un nouvel utilisateur.
        /// </summary>
        /// <returns>true si tous les champs sont valides, false sinon.</returns>
        private bool ValiderChamps()
        {
            // Validation du nom
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNom.Focus();
                return false;
            }

            // Validation de l'email (obligatoire)
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("L'email est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validation du format email (vérification minimale: contient @ et .)
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("L'email n'est pas valide.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Le mot de passe est obligatoire seulement pour la création (pas la modification)
            if (_utilisateurSelectionne == null && string.IsNullOrWhiteSpace(txtMotDePasse.Text))
            {
                MessageBox.Show("Le mot de passe est obligatoire pour un nouvel utilisateur.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMotDePasse.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Déclenché au clic sur "Ajouter".
        /// Valide les champs puis délègue la création au contrôleur.
        /// </summary>
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps()) return;

            var nouvelUtilisateur = GetUtilisateurFromForm();
            _controller.AjouterUtilisateur(nouvelUtilisateur);
            ViderChamps();
        }

        /// <summary>
        /// Déclenché au clic sur "Modifier".
        /// Vérifie qu'un utilisateur est sélectionné, valide les champs
        /// puis délègue la modification au contrôleur.
        /// </summary>
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (_utilisateurSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à modifier.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValiderChamps()) return;

            var utilisateurModifie = GetUtilisateurFromForm();
            _controller.ModifierUtilisateur(utilisateurModifie);
            ViderChamps();
        }

        /// <summary>
        /// Déclenché au clic sur "Supprimer".
        /// Vérifie qu'un utilisateur est sélectionné, puis délègue la suppression (soft delete)
        /// au contrôleur (qui gérera la confirmation et les vérifications de droits).
        /// </summary>
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (_utilisateurSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _controller.SupprimerUtilisateur(_utilisateurSelectionne.Id);
            ViderChamps();
        }

        /// <summary>
        /// Déclenché au clic sur "Annuler".
        /// Vide le formulaire, désélectionne la grille et recharge la liste des utilisateurs.
        /// </summary>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            ViderChamps();
            dgvUtilisateurs.ClearSelection();
            // Rechargement de la liste pour annuler toute modification non appliquée
            _controller.ChargerUtilisateurs();
        }
    }
}