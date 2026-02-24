// =============================================================================
// Contrôleur des utilisateurs (UtilisateurController)
// Gère la logique métier du formulaire de gestion des utilisateurs :
//   - Chargement et affichage de la liste des utilisateurs
//   - Ajout d'un nouvel utilisateur (Admin uniquement)
//   - Modification des informations d'un utilisateur (avec gestion des droits)
//   - Suppression (soft delete) d'un utilisateur (Admin uniquement)
// =============================================================================

using System;
using System.Collections.Generic;
using GestionProjet.Repositories;
using GestionProjet.Models;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur gérant les opérations CRUD sur les utilisateurs.
    /// Applique les règles de droits d'accès selon le rôle de l'utilisateur connecté.
    /// Hérite de <see cref="BaseController"/> pour la navigation et les messages.
    /// </summary>
    public class UtilisateurController : BaseController
    {
        /// <summary>Référence vers le formulaire de gestion des utilisateurs (vue).</summary>
        private readonly UtilisateurForm _utilisateurForm;

        /// <summary>Repository pour accéder aux données des utilisateurs.</summary>
        private readonly IUtilisateurRepository _utilisateurRepository;

        /// <summary>L'utilisateur actuellement connecté (détermine les droits d'accès).</summary>
        private readonly Utilisateur _utilisateurCourant;

        /// <summary>
        /// Initialise le contrôleur des utilisateurs et charge la liste au démarrage.
        /// Configure la vue selon le rôle de l'utilisateur connecté.
        /// </summary>
        /// <param name="utilisateurForm">Le formulaire de gestion des utilisateurs.</param>
        /// <param name="utilisateurCourant">L'utilisateur connecté.</param>
        public UtilisateurController(UtilisateurForm utilisateurForm, Utilisateur utilisateurCourant)
        {
            _utilisateurForm = utilisateurForm;
            _utilisateurCourant = utilisateurCourant;
            _utilisateurRepository = new UtilisateurRepository();
            // Enregistre le contrôleur et transmet l'utilisateur courant à la vue
            _utilisateurForm.SetController(this);
            _utilisateurForm.SetCurrentUser(utilisateurCourant);
            CurrentForm = utilisateurForm;
            // Chargement initial de la liste
            ChargerUtilisateurs();
        }

        /// <summary>
        /// Charge et affiche la liste de tous les utilisateurs actifs.
        /// </summary>
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

        /// <summary>
        /// Ajoute un nouvel utilisateur après vérification de l'unicité de l'email.
        /// </summary>
        /// <param name="utilisateur">Les données du nouvel utilisateur à créer.</param>
        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                // Vérifier si l'email existe déjà pour un autre utilisateur
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

        /// <summary>
        /// Modifie les informations d'un utilisateur existant.
        /// Règles de droits :
        ///   - Un utilisateur normal ne peut modifier que ses propres informations.
        ///   - Un administrateur peut modifier n'importe quel utilisateur.
        /// Vérifie aussi l'unicité de l'email avec les autres utilisateurs.
        /// </summary>
        /// <param name="utilisateur">Les nouvelles données de l'utilisateur.</param>
        public void ModifierUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                // Vérification des droits : un User simple ne peut modifier que lui-même
                if (_utilisateurCourant.Role != "Admin" && _utilisateurCourant.Id != utilisateur.Id)
                {
                    AfficherErreur("Vous n'avez pas l'autorisation de modifier les informations d'un autre utilisateur.");
                    return;
                }

                // Vérifier si l'email existe déjà pour un AUTRE utilisateur (pas lui-même)
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

        /// <summary>
        /// Supprime (soft delete) un utilisateur après validation des droits.
        /// Règles de droits :
        ///   - Seul un administrateur peut supprimer un utilisateur.
        ///   - Un administrateur ne peut pas se supprimer lui-même.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à supprimer.</param>
        public void SupprimerUtilisateur(int id)
        {
            try
            {
                // Seuls les admins peuvent supprimer des utilisateurs
                if (_utilisateurCourant.Role != "Admin")
                {
                    AfficherErreur("Seul un administrateur peut supprimer un utilisateur.");
                    return;
                }

                // L'admin ne peut pas se supprimer lui-même
                if (id == _utilisateurCourant.Id)
                {
                    AfficherErreur("Vous ne pouvez pas vous supprimer vous-même.");
                    return;
                }

                if (ConfirmerAction("Voulez-vous vraiment supprimer cet utilisateur ?"))
                {
                    // Soft delete : désactive le compte sans le supprimer physiquement
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

        /// <summary>
        /// Récupère un utilisateur par son identifiant unique.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur recherché.</param>
        /// <returns>L'utilisateur trouvé ou null en cas d'erreur.</returns>
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