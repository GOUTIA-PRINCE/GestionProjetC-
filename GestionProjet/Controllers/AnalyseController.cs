using System;
using System.Collections.Generic;
using System.Linq;
using GestionProjet.Models;
using GestionProjet.Repositories;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    public class AnalyseController : BaseController
    {
        private readonly AnalyseForm _form;
        private readonly Utilisateur _utilisateur;
        private readonly IProjetRepository _projetRepository;
        private readonly ITacheRepository _tacheRepository;

        public AnalyseController(AnalyseForm form, Utilisateur utilisateur)
        {
            _form = form;
            _utilisateur = utilisateur;
            _projetRepository = new ProjetRepository();
            _tacheRepository = new TacheRepository();

            ChargerDonnees();
        }

        public void ChargerDonnees()
        {
            try
            {
                var projets = _projetRepository.GetByMembre(_utilisateur.Id);
                var allTaches = projets.SelectMany(p => _tacheRepository.GetAllByProjet(p.Id)).ToList();
                var statuts = _tacheRepository.GetStatuts();

                _form.AfficherStatistiques(projets, allTaches, statuts);
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors du chargement des analyses : {ex.Message}");
            }
        }
    }
}
