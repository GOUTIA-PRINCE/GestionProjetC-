using System;
using System.Collections.Generic;

namespace GestionProjet.Models
{
    public class Projet
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateFinPrevue { get; set; }
        public int? CreateurId { get; set; }
        public int Progression { get; set; } // Pourcentage d'avancement

        // Propriété de navigation (facultatif selon votre besoin)
        public Utilisateur Createur { get; set; }
        public List<Tache> Taches { get; set; }

        public Projet()
        {
            DateCreation = DateTime.Now;
            Taches = new List<Tache>();
            Progression = 0;
        }
    }
}
