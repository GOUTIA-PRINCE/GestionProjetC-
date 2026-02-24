// =============================================================================
// Vue - Formulaire de tâche (TacheForm)
// Boîte de dialogue modale pour créer ou modifier une tâche.
// Fonctionne en mode dual :
//   - Tache = null → mode Création (champs vides avec valeurs par défaut)
//   - Tache fournie → mode Modification (pré-remplissage des champs)
// Les ComboBox statut, priorité et assigné sont peuplés depuis des listes
// fournies par le contrôleur (issues de la base de données).
// Retourne DialogResult.OK avec les données saisies si l'utilisateur confirme.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionProjet.Models;

namespace GestionProjet.Views
{
    /// <summary>
    /// Boîte de dialogue modale pour la création ou la modification d'une tâche.
    /// Les données saisies sont accessibles via la propriété <see cref="Tache"/>
    /// après confirmation (DialogResult.OK).
    /// </summary>
    public partial class TacheForm : Form
    {
        /// <summary>
        /// L'objet tâche en cours d'édition.
        /// Peut être une tâche existante (modification) ou une nouvelle (création).
        /// </summary>
        private Tache _tache;

        /// <summary>
        /// Propriété en lecture seule exposant la tâche éditée au contrôleur.
        /// Le contrôleur récupère cette valeur après DialogResult.OK.
        /// </summary>
        public Tache Tache => _tache;

        /// <summary>
        /// Initialise la boîte de dialogue en mode création ou modification.
        /// </summary>
        /// <param name="tache">La tâche à modifier, ou null pour en créer une nouvelle.</param>
        /// <param name="statuts">Liste des statuts disponibles (colonnes Kanban).</param>
        /// <param name="priorites">Liste des priorités disponibles (Basse, Normale, etc.).</param>
        /// <param name="membres">Liste des membres du projet (candidats pour l'assignation).</param>
        public TacheForm(Tache tache, List<Statut> statuts, List<Priorite> priorites, List<Utilisateur> membres)
        {
            InitializeComponent();
            // Si null → nouvelle tâche, sinon → tâche existante à modifier
            _tache = tache ?? new Tache();

            // ── Peuplement des ComboBox depuis les listes fournies ────────────
            // Chaque ComboBox affiche le Libelle/Nom et utilise l'Id comme valeur

            // ComboBox Statut
            cmbStatut.DataSource = statuts;
            cmbStatut.DisplayMember = "Libelle"; // Texte affiché dans le ComboBox
            cmbStatut.ValueMember = "Id";         // Valeur sélectionnée = Id du statut

            // ComboBox Priorité
            cmbPriorite.DataSource = priorites;
            cmbPriorite.DisplayMember = "Libelle";
            cmbPriorite.ValueMember = "Id";

            // ComboBox Assigné (membres du projet)
            cmbAssignee.DataSource = membres;
            cmbAssignee.DisplayMember = "Nom";
            cmbAssignee.ValueMember = "Id";

            if (tache != null)
            {
                // ── Mode Modification : pré-remplissage des champs ──────────────
                lblFormTitre.Text = "Modifier la tâche";
                txtTitre.Text = tache.Titre;
                txtDescription.Text = tache.Description;
                // Pré-sélection des éléments dans les ComboBox selon les Ids de la tâche
                cmbStatut.SelectedValue = tache.StatutId;
                // -1 si PrioriteId est null (aucune priorité sélectionnée)
                cmbPriorite.SelectedValue = tache.PrioriteId ?? -1;
                // -1 si AssigneeId est null (aucun assigné)
                cmbAssignee.SelectedValue = tache.AssigneeId ?? -1;
                // Pré-sélection de la date d'échéance si elle existe
                if (tache.DateEcheance.HasValue)
                    dtpEcheance.Value = tache.DateEcheance.Value;
            }
            else
            {
                // ── Mode Création : titre et date par défaut ───────────────────
                lblFormTitre.Text = "Nouvelle tâche";
                // Date d'échéance suggérée à 7 jours à partir d'aujourd'hui
                dtpEcheance.Value = DateTime.Now.AddDays(7);
            }
        }

        /// <summary>
        /// Déclenché au clic sur "Enregistrer".
        /// Valide que le titre n'est pas vide, puis applique les données
        /// saisies à l'objet tâche et ferme avec DialogResult.OK.
        /// </summary>
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            // Validation : le titre est obligatoire
            if (string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                MessageBox.Show("Le titre est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mise à jour de l'objet tâche avec les données saisies
            _tache.Titre = txtTitre.Text.Trim();
            _tache.Description = txtDescription.Text.Trim();
            // Récupération des Ids sélectionnés depuis les ComboBox
            _tache.StatutId = (int)cmbStatut.SelectedValue;
            _tache.PrioriteId = (int)cmbPriorite.SelectedValue;
            _tache.AssigneeId = (int)cmbAssignee.SelectedValue;
            _tache.DateEcheance = dtpEcheance.Value;

            // Signale au contrôleur que l'utilisateur a confirmé
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Déclenché au clic sur "Annuler".
        /// Ferme la boîte de dialogue sans sauvegarder (DialogResult.Cancel).
        /// </summary>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
