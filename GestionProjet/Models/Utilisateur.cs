using System;

namespace GestionProjet.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public DateTime DateCreation { get; set; }
        public bool EstActif { get; set; }
        public string Role { get; set; } // "Admin" ou "User"

        // Constructeur par défaut
        public Utilisateur()
        {
            DateCreation = DateTime.Now;
            EstActif = true;
        }

        // Constructeur avec paramètres
        public Utilisateur(string nom, string email, string motDePasse)
        {
            Nom = nom;
            Email = email;
            MotDePasse = motDePasse;
            DateCreation = DateTime.Now;
            EstActif = true;
        }
    }
}