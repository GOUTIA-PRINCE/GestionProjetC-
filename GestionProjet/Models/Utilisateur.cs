// =============================================================================
// Modèle Utilisateur
// Représente un utilisateur du système de gestion de projet.
// Chaque utilisateur possède un identifiant, un nom, un email, un mot de passe
// (haché côté base de données), un rôle et un statut actif/inactif.
// =============================================================================

using System;

namespace GestionProjet.Models
{
    /// <summary>
    /// Modèle représentant un utilisateur de l'application.
    /// Le rôle peut être "Admin" ou "User".
    /// </summary>
    public class Utilisateur
    {
        /// <summary>Identifiant unique de l'utilisateur (clé primaire en base).</summary>
        public int Id { get; set; }

        /// <summary>Nom complet de l'utilisateur.</summary>
        public string Nom { get; set; }

        /// <summary>Adresse email de l'utilisateur (doit être unique).</summary>
        public string Email { get; set; }

        /// <summary>
        /// Mot de passe en clair (utilisé uniquement lors de la création/connexion).
        /// En base de données, il est stocké sous forme hachée SHA-256.
        /// </summary>
        public string MotDePasse { get; set; }

        /// <summary>Date et heure de création du compte.</summary>
        public DateTime DateCreation { get; set; }

        /// <summary>
        /// Indique si le compte est actif. Un compte désactivé (soft delete)
        /// ne peut plus se connecter mais reste en base de données.
        /// </summary>
        public bool EstActif { get; set; }

        /// <summary>
        /// Rôle de l'utilisateur dans le système : "Admin" ou "User".
        /// Les administrateurs ont accès à toutes les fonctionnalités.
        /// </summary>
        public string Role { get; set; } // "Admin" ou "User"

        /// <summary>
        /// Constructeur par défaut.
        /// Initialise la date de création à maintenant et active le compte.
        /// </summary>
        public Utilisateur()
        {
            DateCreation = DateTime.Now;
            EstActif = true;
        }

        /// <summary>
        /// Constructeur avec les informations essentielles d'un nouvel utilisateur.
        /// </summary>
        /// <param name="nom">Nom complet de l'utilisateur.</param>
        /// <param name="email">Adresse email de l'utilisateur.</param>
        /// <param name="motDePasse">Mot de passe en clair (sera haché à la persistance).</param>
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