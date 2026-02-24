using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Repositories
{
    public interface IProjetRepository
    {
        List<Projet> GetAll();
        List<Projet> GetByMembre(int utilisateurId);
        Projet GetById(int id);
        void Add(Projet projet);
        void Update(Projet projet);
        void Delete(int id);
        void AjouterMembre(int projetId, int utilisateurId, string role);
        void RetirerMembre(int projetId, int utilisateurId);
        List<Utilisateur> GetMembres(int projetId);
    }
}
