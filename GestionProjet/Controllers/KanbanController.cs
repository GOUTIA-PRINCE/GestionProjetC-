using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    public class KanbanController : BaseController
    {
        private readonly KanbanForm _kanbanForm;
        private readonly ITacheRepository _tacheRepository;
        private readonly Projet _projet;
        private readonly Utilisateur _utilisateurCourant;

        public KanbanController(KanbanForm kanbanForm, Projet projet, Utilisateur utilisateur)
        {
            _kanbanForm = kanbanForm;
            _projet = projet;
            _utilisateurCourant = utilisateur;
            _tacheRepository = new TacheRepository();
            
            _kanbanForm.SetController(this, _projet);
            CurrentForm = kanbanForm;
            
            InitialiserKanban();
        }

        public void InitialiserKanban()
        {
            try
            {
                var statuts = _tacheRepository.GetStatuts();
                _kanbanForm.InitialiserColonnes(statuts);
                ChargerTaches();
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de l'initialisation du Kanban : {ex.Message}");
            }
        }

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

        public void AjouterTache()
        {
            // Ici, vous pourriez ouvrir un TacheForm pour saisir les détails
            // Pour l'exemple, on crée une tâche rapide
            string titre = Microsoft.VisualBasic.Interaction.InputBox("Titre de la tâche :", "Nouvelle Tâche", "");
            if (!string.IsNullOrWhiteSpace(titre))
            {
                var nouvelleTache = new Tache
                {
                    Titre = titre,
                    ProjetId = _projet.Id,
                    StatutId = 1, // "À faire"
                    AssigneeId = _utilisateurCourant.Id
                };
                
                _tacheRepository.Add(nouvelleTache);
                ChargerTaches();
            }
        }

        public void ModifierTache(Tache tache)
        {
            // Permet de changer de statut par exemple
            var statuts = _tacheRepository.GetStatuts();
            // (Logique simplifiée pour changer de colonne en cliquant)
            int indexActuel = statuts.FindIndex(s => s.Id == tache.StatutId);
            int prochainIndex = (indexActuel + 1) % statuts.Count;
            
            _tacheRepository.UpdateStatut(tache.Id, statuts[prochainIndex].Id);
            ChargerTaches();
        }

        public void RetourProjets()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            NaviguerVers(projetForm);
        }
    }
}
