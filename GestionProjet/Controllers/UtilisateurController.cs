using System;
using System.Collections.Generic;
using GestionProjet.Repositories;
using GestionProjet.Models;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    public class UtilisateurController : BaseController
    {
        private readonly UtilisateurForm _utilisateurForm;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public UtilisateurController(UtilisateurForm utilisateurForm)
        {
            _utilisateurForm = utilisateurForm;
            _utilisateurRepository = new UtilisateurRepository();
            _utilisateurForm.SetController(this);
            CurrentForm = utilisateurForm;
            ChargerUtilisateurs();
        }

        public void ChargerUtilisateurs()
        {
            try
            {
                var utilisateurs = _utilisateurRepository.GetAll();
                _utilisateurForm.AfficherUtilisateurs(utilisateurs);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du chargement : {ex.Message}");
            }
        }

        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                // Vérifier si l'email existe déjà
                var existant = _utilisateurRepository.GetByEmail(utilisateur.Email);
                if (existant != null)
                {
                    AfficherErreur("Cet email est déjà utilisé par un autre utilisateur");
                    return;
                }

                _utilisateurRepository.Add(utilisateur);
                ChargerUtilisateurs();
                AfficherMessage("Utilisateur ajouté avec succès");
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de l'ajout : {ex.Message}");
            }
        }

        public void ModifierUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                // Vérifier si l'email existe déjà pour un autre utilisateur
                var existant = _utilisateurRepository.GetByEmail(utilisateur.Email);
                if (existant != null && existant.Id != utilisateur.Id)
                {
                    AfficherErreur("Cet email est déjà utilisé par un autre utilisateur");
                    return;
                }

                _utilisateurRepository.Update(utilisateur);
                ChargerUtilisateurs();
                AfficherMessage("Utilisateur modifié avec succès");
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la modification : {ex.Message}");
            }
        }

        public void SupprimerUtilisateur(int id)
        {
            try
            {
                if (ConfirmerAction("Voulez-vous vraiment supprimer cet utilisateur ?"))
                {
                    _utilisateurRepository.Delete(id);
                    ChargerUtilisateurs();
                    AfficherMessage("Utilisateur supprimé avec succès");
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la suppression : {ex.Message}");
            }
        }

        public Utilisateur GetUtilisateurById(int id)
        {
            try
            {
                return _utilisateurRepository.GetById(id);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la récupération : {ex.Message}");
                return null;
            }
        }
    }
}