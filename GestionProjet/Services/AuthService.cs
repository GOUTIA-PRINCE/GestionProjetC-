using GestionProjet.Models;
using GestionProjet.Repositories;

namespace GestionProjet.Services
{
    public class AuthService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        private Utilisateur _utilisateurCourant;

        public AuthService()
        {
            _utilisateurRepository = new UtilisateurRepository();
        }

        public Utilisateur UtilisateurCourant => _utilisateurCourant;

        public bool EstConnecte => _utilisateurCourant != null;

        public bool Connexion(string email, string motDePasse)
        {
            _utilisateurCourant = _utilisateurRepository.Authentifier(email, motDePasse);
            return EstConnecte;
        }

        public void Deconnexion()
        {
            _utilisateurCourant = null;
        }
    }
}