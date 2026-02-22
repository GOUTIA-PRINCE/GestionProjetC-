
using System;
using GestionProjet.Repositories;
using GestionProjet.Models;
using GestionProjet.Views;
using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly RegisterForm _registerForm;
        private readonly IUtilisateurRepository _utilisateurRepository;

        public RegisterController(RegisterForm registerForm)
        {
            _registerForm = registerForm;
            _utilisateurRepository = new UtilisateurRepository();
            _registerForm.SetController(this);
            CurrentForm = registerForm;
        }

        public void Inscrire(string nom, string email, string motDePasse)
        {
            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(motDePasse))
            {
                AfficherErreur("Veuillez remplir tous les champs", "Erreur de saisie");
                return;
            }

            try
            {
                var existant = _utilisateurRepository.GetByEmail(email);
                if (existant != null)
                {
                    AfficherErreur("Cet email est déjà utilisé", "Erreur");
                    return;
                }

                var nouvelUtilisateur = new Utilisateur
                {
                    Nom = nom,
                    Email = email,
                    MotDePasse = motDePasse,
                    EstActif = true,
                    DateCreation = DateTime.Now
                };

                _utilisateurRepository.Add(nouvelUtilisateur);
                AfficherMessage("Compte créé avec succès ! Vous pouvez maintenant vous connecter.", "Succès");
                
                RetourLogin();
            }
            catch (Exception ex)
            {
                AfficherErreur($"Erreur lors de la création du compte : {ex.Message}");
            }
        }

        public void RetourLogin()
        {
            var loginForm = new LoginForm();
            var loginController = new LoginController(loginForm);
            NaviguerVers(loginForm);
        }
    }
}
