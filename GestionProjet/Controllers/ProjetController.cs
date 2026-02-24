// =============================================================================
// Contrôleur des projets (ProjetController)
// Gère la logique métier du formulaire de gestion des projets :
//   - Chargement et affichage de la liste des projets
//   - Ajout d'un projet + gestion des membres sélectionnés
//   - Modification d'un projet + mise à jour des membres
//   - Suppression d'un projet
//   - Navigation vers le tableau Kanban
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur gérant les opérations CRUD sur les projets et leurs membres.
    /// Hérite de <see cref="BaseController"/> pour la navigation et les messages.
    /// </summary>
    public class ProjetController : BaseController
    {
        /// <summary>Référence vers le formulaire de gestion des projets (vue).</summary>
        private readonly ProjetForm _projetForm;

        /// <summary>Repository pour accéder aux données des projets.</summary>
        private readonly IProjetRepository _projetRepository;

        /// <summary>Repository pour récupérer la liste de tous les utilisateurs.</summary>
        private readonly IUtilisateurRepository _utilisateurRepository;

        /// <summary>L'utilisateur actuellement connecté.</summary>
        private readonly Utilisateur _utilisateurCourant;

        /// <summary>
        /// Initialise le contrôleur des projets, enregistre la vue et charge les projets.
        /// </summary>
        /// <param name="projetForm">Le formulaire de gestion des projets à contrôler.</param>
        /// <param name="utilisateur">L'utilisateur connecté.</param>
        public ProjetController(ProjetForm projetForm, Utilisateur utilisateur)
        {
            _projetForm = projetForm;
            _utilisateurCourant = utilisateur;
            _projetRepository = new ProjetRepository();
            _utilisateurRepository = new UtilisateurRepository();

            _projetForm.SetController(this);
            CurrentForm = projetForm;

            ChargerProjets();
        }

        /// <summary>
        /// Charge et affiche tous les projets dont l'utilisateur courant est membre.
        /// </summary>
        public void ChargerProjets()
        {
            try
            {
                var projets = _projetRepository.GetByMembre(_utilisateurCourant.Id);
                _projetForm.AfficherProjets(projets);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du chargement des projets : {ex.Message}");
            }
        }

        /// <summary>
        /// Ouvre le formulaire de création d'un projet avec la liste de tous les utilisateurs.
        /// Après confirmation :
        ///   1. Insère le projet en base (le créateur devient membre "Admin" automatiquement)
        ///   2. Ajoute chaque membre sélectionné avec le rôle "Membre"
        ///      (INSERT IGNORE évite les doublons si le créateur est aussi sélectionné)
        /// </summary>
        public void AjouterProjet()
        {
            try
            {
                // Récupère tous les utilisateurs pour peupler la ListBox gauche
                var tousUtilisateurs = _utilisateurRepository.GetAll();

                using (var form = new ProjetEditForm(null, tousUtilisateurs, null))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var projet = form.Projet;
                        projet.CreateurId = _utilisateurCourant.Id;

                        // Étape 1 : insertion du projet (le repository ajoute aussi le créateur comme "Admin")
                        _projetRepository.Add(projet);

                        // Étape 2 : ajouter tous les membres sélectionnés
                        // INSERT IGNORE gère automatiquement le cas où le créateur est aussi dans la liste
                        foreach (var membre in form.MembresSelectionnes)
                        {
                            _projetRepository.AjouterMembre(projet.Id, membre.Id, "Membre");
                        }

                        ChargerProjets();
                        AfficherMessage($"Projet '{projet.Nom}' créé avec succès !");
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la création : {ex.Message}");
            }
        }

        /// <summary>
        /// Ouvre le formulaire de modification d'un projet existant.
        /// Pré-remplit les deux ListBox : utilisateurs disponibles et membres actuels.
        /// Après confirmation, synchronise les membres :
        ///   - Supprime tous les anciens membres (sauf le créateur)
        ///   - Réinsère les membres nouvellement sélectionnés
        /// </summary>
        /// <param name="projet">Le projet à modifier.</param>
        public void ModifierProjet(Projet projet)
        {
            try
            {
                // Récupère tous les utilisateurs et les membres actuels du projet
                var tousUtilisateurs = _utilisateurRepository.GetAll();
                var membresActuels = _projetRepository.GetMembres(projet.Id);

                using (var form = new ProjetEditForm(projet, tousUtilisateurs, membresActuels))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Mise à jour des infos du projet
                        _projetRepository.Update(form.Projet);

                        // Synchronisation des membres :
                        // On retire d'abord tous les membres SAUF le créateur (qui reste toujours "Admin")
                        foreach (var ancien in membresActuels)
                        {
                            if (ancien.Id != projet.CreateurId)
                                _projetRepository.RetirerMembre(projet.Id, ancien.Id);
                        }

                        // Puis on réinsère les membres sélectionnés dans le formulaire
                        foreach (var membre in form.MembresSelectionnes)
                        {
                            _projetRepository.AjouterMembre(projet.Id, membre.Id, "Membre");
                        }

                        ChargerProjets();
                        AfficherMessage($"Projet '{form.Projet.Nom}' mis à jour !");
                    }
                }
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la modification : {ex.Message}");
            }
        }

        /// <summary>
        /// Demande confirmation et supprime définitivement un projet (et ses tâches/membres).
        /// </summary>
        /// <param name="projet">Le projet à supprimer.</param>
        public void SupprimerProjet(Projet projet)
        {
            try
            {
                if (ConfirmerAction($"Voulez-vous vraiment supprimer le projet '{projet.Nom}' ?\nCela supprimera également toutes ses tâches et ses membres."))
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

        /// <summary>
        /// Navigue vers le tableau Kanban du projet sélectionné.
        /// </summary>
        /// <param name="projet">Le projet dont on veut afficher le Kanban.</param>
        public void OuvrirKanban(Projet projet)
        {
            var kanbanForm = new KanbanForm();
            var kanbanController = new KanbanController(kanbanForm, projet, _utilisateurCourant);
            NaviguerVers(kanbanForm);
        }
    }
}
