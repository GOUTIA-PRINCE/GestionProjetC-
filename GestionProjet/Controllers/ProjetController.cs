using System;
using System.Collections.Generic;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    public class ProjetController : BaseController
    {
        private readonly ProjetForm _projetForm;
        private readonly IProjetRepository _projetRepository;
        private readonly Utilisateur _utilisateurCourant;

        public ProjetController(ProjetForm projetForm, Utilisateur utilisateur)
        {
            _projetForm = projetForm;
            _utilisateurCourant = utilisateur;
            _projetRepository = new ProjetRepository();
            
            _projetForm.SetController(this);
            CurrentForm = projetForm;
            
            ChargerProjets();
        }

        public void ChargerProjets()
        {
            try
            {
                // On récupère les projets où l'utilisateur est membre
                var projets = _projetRepository.GetByMembre(_utilisateurCourant.Id);
                _projetForm.AfficherProjets(projets);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du chargement des projets : {ex.Message}");
            }
        }

        public void CreerProjet(string nom, string description)
        {
            try
            {
                var projet = new Projet
                {
                    Nom = nom,
                    Description = description,
                    CreateurId = _utilisateurCourant.Id
                };
                
                _projetRepository.Add(projet);
                ChargerProjets();
                AfficherMessage("Projet créé avec succès !");
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la création : {ex.Message}");
            }
        }

        public void OuvrirKanban(Projet projet)
        {
            var kanbanForm = new KanbanForm();
            var kanbanController = new KanbanController(kanbanForm, projet, _utilisateurCourant);
            NaviguerVers(kanbanForm);
        }
    }
}
