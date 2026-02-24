// =============================================================================
// Contrôleur d'analyse (AnalyseController)
// Charge les données agrégées (projets, tâches, statuts) de l'utilisateur
// courant et les transmet au formulaire d'analyse pour affichage graphique.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Contrôleur du module d'analyse et de statistiques.
    /// Agrège les données de tous les projets et tâches de l'utilisateur,
    /// puis les transmet à la vue <see cref="AnalyseForm"/> pour affichage.
    /// </summary>
    public class AnalyseController : BaseController
    {
        /// <summary>Référence vers le formulaire d'analyse (vue).</summary>
        private readonly AnalyseForm _form;

        /// <summary>L'utilisateur pour lequel on génère les statistiques.</summary>
        private readonly Utilisateur _utilisateur;

        /// <summary>Repository pour accéder aux projets.</summary>
        private readonly IProjetRepository _projetRepository;

        /// <summary>Repository pour accéder aux tâches et statuts.</summary>
        private readonly ITacheRepository _tacheRepository;

        /// <summary>
        /// Initialise le contrôleur d'analyse et charge immédiatement les données.
        /// </summary>
        /// <param name="form">Le formulaire d'analyse à contrôler.</param>
        /// <param name="utilisateur">L'utilisateur connecté (pour filtrer ses projets).</param>
        public AnalyseController(AnalyseForm form, Utilisateur utilisateur)
        {
            _form = form;
            _utilisateur = utilisateur;
            _projetRepository = new ProjetRepository();
            _tacheRepository = new TacheRepository();

            // Charge et affiche les données immédiatement à l'ouverture
            ChargerDonnees();
        }

        /// <summary>
        /// Charge les projets et tâches de l'utilisateur, récupère les statuts
        /// et transmet tout au formulaire d'analyse pour affichage graphique.
        /// Les tâches de tous les projets sont agrégées en une seule liste.
        /// </summary>
        public void ChargerDonnees()
        {
            try
            {
                // Récupère tous les projets dont l'utilisateur est membre
                var projets = _projetRepository.GetByMembre(_utilisateur.Id);

                // Agrège toutes les tâches de tous les projets en une seule liste (LINQ SelectMany)
                var allTaches = projets.SelectMany(p => _tacheRepository.GetAllByProjet(p.Id)).ToList();

                // Récupère la liste des statuts pour le graphique de répartition
                var statuts = _tacheRepository.GetStatuts();

                // Transmet les données à la vue pour affichage
                _form.AfficherStatistiques(projets, allTaches, statuts);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du chargement des analyses : {ex.Message}");
            }
        }
    }
}
