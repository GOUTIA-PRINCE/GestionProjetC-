// =============================================================================
// Contrôleur Kanban (KanbanController)
// Gère la logique métier du tableau Kanban d'un projet :
//   - Initialisation des colonnes selon les statuts disponibles
//   - Chargement et affichage des tâches
//   - Ajout, modification, suppression de tâches
//   - Changement de statut par drag-and-drop
//   - Retour vers la liste des projets
// =============================================================================

using System;
using System.Windows.Forms;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur gérant le tableau Kanban d'un projet spécifique.
    /// Hérite de <see cref="BaseController"/> pour la navigation et les messages.
    /// </summary>
    public class KanbanController : BaseController
    {
        /// <summary>Référence vers le formulaire Kanban (vue).</summary>
        private readonly KanbanForm _kanbanForm;

        /// <summary>Repository pour accéder aux données des tâches.</summary>
        private readonly ITacheRepository _tacheRepository;

        /// <summary>Repository pour accéder aux données des projets (membres).</summary>
        private readonly IProjetRepository _projetRepository;

        /// <summary>Repository pour récupérer tous les utilisateurs actifs.</summary>
        private readonly IUtilisateurRepository _utilisateurRepository;

        /// <summary>Le projet dont on affiche le tableau Kanban.</summary>
        private readonly Projet _projet;

        /// <summary>L'utilisateur actuellement connecté.</summary>
        private readonly Utilisateur _utilisateurCourant;

        /// <summary>
        /// Initialise le contrôleur Kanban pour un projet donné.
        /// Enregistre la vue, initialise les colonnes et charge les tâches.
        /// </summary>
        /// <param name="kanbanForm">Le formulaire Kanban à contrôler.</param>
        /// <param name="projet">Le projet à afficher dans le Kanban.</param>
        /// <param name="utilisateur">L'utilisateur connecté.</param>
        public KanbanController(KanbanForm kanbanForm, Projet projet, Utilisateur utilisateur)
        {
            _kanbanForm = kanbanForm;
            _projet = projet;
            _utilisateurCourant = utilisateur;
            _tacheRepository = new TacheRepository();
            _projetRepository = new ProjetRepository();
            _utilisateurRepository = new UtilisateurRepository();
            
            // Enregistre le contrôleur et le projet dans la vue
            _kanbanForm.SetController(this, _projet);
            CurrentForm = kanbanForm;
            
            // Initialise les colonnes et charge les tâches
            InitialiserKanban();
        }

        /// <summary>
        /// Initialise le tableau Kanban en créant les colonnes selon les statuts
        /// disponibles en base de données, puis charge les tâches du projet.
        /// </summary>
        public void InitialiserKanban()
        {
            try
            {
                // Récupère la liste des statuts (colonnes Kanban) depuis la base
                var statuts = _tacheRepository.GetStatuts();
                _kanbanForm.InitialiserColonnes(statuts);
                // Charge les tâches et les place dans les bonnes colonnes
                ChargerTaches();
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de l'initialisation du Kanban : {ex.Message}");
            }
        }

        /// <summary>
        /// Récupère toutes les tâches du projet et les affiche dans leurs colonnes respectives.
        /// Appelé après chaque modification pour rafraîchir l'affichage.
        /// </summary>
        public void ChargerTaches()
        {
            try
            {
                var taches = _tacheRepository.GetAllByProjet(_projet.Id);
                _kanbanForm.AfficherTaches(taches);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du chargement des tâches : {ex.Message}");
            }
        }

        /// <summary>
        /// Ouvre le formulaire de création d'une nouvelle tâche (modal).
        /// Inclut les listes de statuts, priorités et membres disponibles pour le projet.
        /// </summary>
        public void AjouterTache()
        {
            try
            {
                // Récupération des données de référentiel pour peupler les ComboBox
                var statuts   = _tacheRepository.GetStatuts();
                var priorites = _tacheRepository.GetPriorites();
                // Seuls les membres du projet peuvent se voir attribuer une tâche
                var membres = _projetRepository.GetMembres(_projet.Id);

                using (var form = new TacheForm(null, statuts, priorites, membres))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var nouvelleTache = form.Tache;
                        // Association de la tâche au projet courant
                        nouvelleTache.ProjetId = _projet.Id;
                        _tacheRepository.Add(nouvelleTache);
                        // Rafraîchissement du Kanban après ajout
                        ChargerTaches();
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de l'ajout : {ex.Message}");
            }
        }

        /// <summary>
        /// Ouvre le formulaire de modification d'une tâche existante (modal).
        /// Pré-remplit le formulaire avec les données actuelles de la tâche.
        /// Aussi utilisé comme "Détails de la tâche" lors d'un clic sur une carte.
        /// </summary>
        /// <param name="tache">La tâche à modifier.</param>
        public void ModifierTache(Tache tache)
        {
            try
            {
                // Récupération des données de référentiel pour peupler les ComboBox
                var statuts   = _tacheRepository.GetStatuts();
                var priorites = _tacheRepository.GetPriorites();
                // Seuls les membres du projet peuvent se voir attribuer une tâche
                var membres = _projetRepository.GetMembres(_projet.Id);

                // TacheForm est initialisé avec la tâche existante (pré-remplissage)
                using (var form = new TacheForm(tache, statuts, priorites, membres))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _tacheRepository.Update(form.Tache);
                        // Rafraîchissement du Kanban après modification
                        ChargerTaches();
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la modification : {ex.Message}");
            }
        }

        /// <summary>
        /// Change le statut d'une tâche suite à un drag-and-drop entre colonnes.
        /// Met à jour uniquement le statut (requête SQL optimisée).
        /// </summary>
        /// <param name="tache">La tâche dont le statut change.</param>
        /// <param name="nouveauStatutId">Identifiant du nouveau statut (colonne de destination).</param>
        public void ChangerStatutTache(Tache tache, int nouveauStatutId)
        {
            try
            {
                // Mise à jour rapide du statut sans recharger toute la tâche
                _tacheRepository.UpdateStatut(tache.Id, nouveauStatutId);
                // Rafraîchissement du Kanban pour refléter le déplacement
                ChargerTaches();
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du changement de statut : {ex.Message}");
            }
        }

        /// <summary>
        /// Demande confirmation et supprime définitivement une tâche.
        /// </summary>
        /// <param name="tache">La tâche à supprimer.</param>
        public void SupprimerTache(Tache tache)
        {
            try
            {
                if (ConfirmerAction($"Voulez-vous vraiment supprimer la tâche '{tache.Titre}' ?"))
                {
                    _tacheRepository.Delete(tache.Id);
                    ChargerTaches();
                    AfficherMessage("Tâche supprimée !");
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la suppression : {ex.Message}");
            }
        }

        /// <summary>
        /// Retourne à la liste des projets en créant un nouveau ProjetForm
        /// et son contrôleur associé.
        /// </summary>
        public void RetourProjets()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            NaviguerVers(projetForm);
        }
    }
}
