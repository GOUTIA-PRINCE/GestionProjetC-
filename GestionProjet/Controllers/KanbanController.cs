using System;
using System.Windows.Forms;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    public class KanbanController : BaseController
    {
        private readonly KanbanForm _kanbanForm;
        private readonly ITacheRepository _tacheRepository;
        private readonly IProjetRepository _projetRepository;
        private readonly Projet _projet;
        private readonly Utilisateur _utilisateurCourant;

        public KanbanController(KanbanForm kanbanForm, Projet projet, Utilisateur utilisateur)
        {
            _kanbanForm = kanbanForm;
            _projet = projet;
            _utilisateurCourant = utilisateur;
            _tacheRepository = new TacheRepository();
            _projetRepository = new ProjetRepository();
            
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
            try
            {
                var statuts = _tacheRepository.GetStatuts();
                var priorites = _tacheRepository.GetPriorites();
                var membres = _projetRepository.GetMembres(_projet.Id);

                using (var form = new TacheForm(null, statuts, priorites, membres))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var nouvelleTache = form.Tache;
                        nouvelleTache.ProjetId = _projet.Id;
                        _tacheRepository.Add(nouvelleTache);
                        ChargerTaches();
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de l'ajout : {ex.Message}");
            }
        }

        public void ModifierTache(Tache tache)
        {
            try
            {
                var statuts = _tacheRepository.GetStatuts();
                var priorites = _tacheRepository.GetPriorites();
                var membres = _projetRepository.GetMembres(_projet.Id);

                using (var form = new TacheForm(tache, statuts, priorites, membres))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _tacheRepository.Update(form.Tache);
                        ChargerTaches();
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la modification : {ex.Message}");
            }
        }

        public void ChangerStatutTache(Tache tache, int nouveauStatutId)
        {
            try
            {
                _tacheRepository.UpdateStatut(tache.Id, nouveauStatutId);
                ChargerTaches();
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du changement de statut : {ex.Message}");
            }
        }

        public void RetourProjets()
        {
            var projetForm = new ProjetForm();
            var projetController = new ProjetController(projetForm, _utilisateurCourant);
            NaviguerVers(projetForm);
        }
    }
}
