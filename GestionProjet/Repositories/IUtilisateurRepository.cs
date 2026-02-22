using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Repositories
{
    public interface IUtilisateurRepository
    {
        // READ
        List<Utilisateur> GetAll();
        Utilisateur GetById(int id);
        Utilisateur GetByEmail(string email);

        // CREATE
        void Add(Utilisateur utilisateur);

        // UPDATE
        void Update(Utilisateur utilisateur);

        // DELETE
        void Delete(int id);

        // AUTHENTIFICATION
        Utilisateur Authentifier(string email, string motDePasse);
    }
}