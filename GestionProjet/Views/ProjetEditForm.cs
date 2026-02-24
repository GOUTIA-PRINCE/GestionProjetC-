// =============================================================================
// Vue - Formulaire d'édition de projet (ProjetEditForm)
// Boîte de dialogue modale pour créer ou modifier un projet.
// Permet aussi de gérer les membres du projet :
//   - ListBox gauche : tous les utilisateurs disponibles
//   - ListBox droite : membres sélectionnés pour ce projet
//   - Boutons ► / ◄ pour déplacer des utilisateurs entre les deux listes
//   - Double-clic sur un utilisateur pour le déplacer rapidement
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    /// <summary>
    /// Boîte de dialogue modale pour la création ou la modification d'un projet,
    /// incluant la sélection des membres participants.
    /// Expose <see cref="Projet"/> et <see cref="MembresSelectionnes"/> après DialogResult.OK.
    /// </summary>
    public partial class ProjetEditForm : Form
    {
        /// <summary>L'objet projet en cours d'édition (nouveau ou existant).</summary>
        private Projet _projet;

        /// <summary>
        /// Propriété en lecture seule exposant le projet édité au contrôleur.
        /// </summary>
        public Projet Projet => _projet;

        /// <summary>
        /// Liste des utilisateurs sélectionnés comme membres du projet.
        /// Exposée au contrôleur après DialogResult.OK pour persister les membres.
        /// </summary>
        public List<Utilisateur> MembresSelectionnes { get; private set; } = new List<Utilisateur>();

        /// <summary>
        /// Initialise la boîte de dialogue en mode création ou modification.
        /// </summary>
        /// <param name="projet">Le projet à modifier, ou null pour en créer un nouveau.</param>
        /// <param name="tousUtilisateurs">
        /// Liste de tous les utilisateurs du système (pour la ListBox gauche).
        /// Null si non fournie (la section membres sera désactivée).
        /// </param>
        /// <param name="membresActuels">
        /// Membres déjà dans le projet (pré-remplissage de la ListBox droite en mode modification).
        /// </param>
        public ProjetEditForm(Projet projet = null,
                              List<Utilisateur> tousUtilisateurs = null,
                              List<Utilisateur> membresActuels = null)
        {
            InitializeComponent();
            _projet = projet ?? new Projet();

            // ── Mode Modification vs Création ─────────────────────────────────
            if (projet != null)
            {
                lblTitre.Text = "Modifier le Projet";
                txtNom.Text = projet.Nom;
                txtDescription.Text = projet.Description;
                if (projet.DateFinPrevue.HasValue)
                    dtpDateFin.Value = projet.DateFinPrevue.Value;
            }
            else
            {
                lblTitre.Text = "Nouveau Projet";
                dtpDateFin.Value = DateTime.Now.AddMonths(1);
            }

            // ── Peuplement des ListBox membres ────────────────────────────────
            if (tousUtilisateurs != null)
            {
                // ListBox gauche : TOUS les utilisateurs de la base (sans filtrage)
                foreach (var u in tousUtilisateurs)
                {
                    lstTousUtilisateurs.Items.Add(u);
                }

                // ListBox droite : membres actuels pré-remplis (mode modification)
                if (membresActuels != null)
                {
                    foreach (var m in membresActuels)
                    {
                        lstMembres.Items.Add(m);
                        MembresSelectionnes.Add(m);
                    }
                }

                // Affichage du nom dans les ListBox via DisplayMember
                lstTousUtilisateurs.DisplayMember = "Nom";
                lstMembres.DisplayMember = "Nom";
            }
            else
            {
                // Pas de liste fournie : désactiver la section membres
                lstTousUtilisateurs.Enabled = false;
                lstMembres.Enabled = false;
                btnAjouterMembre.Enabled = false;
                btnRetirerMembre.Enabled = false;
            }
        }

        // ── Gestion des membres ───────────────────────────────────────────────

        /// <summary>
        /// Ajoute les utilisateurs sélectionnés dans la ListBox droite (membres du projet).
        /// Vérifie qu'ils ne sont pas déjà membres pour éviter les doublons.
        /// La ListBox gauche n'est PAS modifiée : tous les utilisateurs restent visibles.
        /// </summary>
        private void btnAjouterMembre_Click(object sender, EventArgs e)
        {
            var selection = lstTousUtilisateurs.SelectedItems.Cast<Utilisateur>().ToList();
            foreach (var u in selection)
            {
                // Vérifie qu'il n'est pas déjà dans la liste des membres
                bool dejaPresent = lstMembres.Items.Cast<Utilisateur>().Any(m => m.Id == u.Id);
                if (!dejaPresent)
                {
                    lstMembres.Items.Add(u);
                    MembresSelectionnes.Add(u);
                }
            }
        }

        /// <summary>
        /// Retire les utilisateurs sélectionnés de la ListBox droite (membres du projet).
        /// La ListBox gauche reste inchangée car elle affiche toujours tous les utilisateurs.
        /// </summary>
        private void btnRetirerMembre_Click(object sender, EventArgs e)
        {
            var selection = lstMembres.SelectedItems.Cast<Utilisateur>().ToList();
            foreach (var u in selection)
            {
                lstMembres.Items.Remove(u);
                MembresSelectionnes.RemoveAll(m => m.Id == u.Id);
            }
        }

        /// <summary>
        /// Double-clic sur la ListBox gauche : ajoute rapidement l'utilisateur
        /// comme membre si pas déjà présent.
        /// </summary>
        private void lstTousUtilisateurs_DoubleClick(object sender, EventArgs e)
        {
            if (lstTousUtilisateurs.SelectedItem is Utilisateur u)
            {
                bool dejaPresent = lstMembres.Items.Cast<Utilisateur>().Any(m => m.Id == u.Id);
                if (!dejaPresent)
                {
                    lstMembres.Items.Add(u);
                    MembresSelectionnes.Add(u);
                }
            }
        }

        /// <summary>
        /// Double-clic sur la ListBox droite : retire rapidement le membre.
        /// La liste gauche reste inchangée.
        /// </summary>
        private void lstMembres_DoubleClick(object sender, EventArgs e)
        {
            if (lstMembres.SelectedItem is Utilisateur u)
            {
                lstMembres.Items.Remove(u);
                MembresSelectionnes.RemoveAll(m => m.Id == u.Id);
            }
        }

        // ── Validation et fermeture ───────────────────────────────────────────

        /// <summary>
        /// Déclenché au clic sur "Enregistrer".
        /// Valide le nom, met à jour l'objet projet et ferme avec DialogResult.OK.
        /// </summary>
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom du projet est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _projet.Nom = txtNom.Text.Trim();
            _projet.Description = txtDescription.Text.Trim();
            _projet.DateFinPrevue = dtpDateFin.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Déclenché au clic sur "Annuler". Ferme sans sauvegarder.
        /// </summary>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
