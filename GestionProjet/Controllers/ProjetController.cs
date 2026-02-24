using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

        public void AjouterProjet()
        {
            try
            {
                using (var form = new ProjetEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var projet = form.Projet;
                        projet.CreateurId = _utilisateurCourant.Id;
                        _projetRepository.Add(projet);
                        
                        // Ajouter le créateur comme membre par défaut
                        _projetRepository.AjouterMembre(projet.Id, _utilisateurCourant.Id, "Propriétaire");
                        
                        ChargerProjets();
                        AfficherMessage("Projet créé avec succès !");
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la création : {ex.Message}");
            }
        }

        public void ModifierProjet(Projet projet)
        {
            try
            {
                using (var form = new ProjetEditForm(projet))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _projetRepository.Update(form.Projet);
                        ChargerProjets();
                        AfficherMessage("Projet mis à jour !");
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la modification : {ex.Message}");
            }
        }

        public void SupprimerProjet(Projet projet)
        {
            try
            {
                if (ConfirmerAction($"Voulez-vous vraiment supprimer le projet '{projet.Nom}' ? Cela supprimera également toutes ses tâches."))
                {
                    _projetRepository.Delete(projet.Id);
                    ChargerProjets();
                    AfficherMessage("Projet supprimé !");
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la suppression : {ex.Message}");
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
