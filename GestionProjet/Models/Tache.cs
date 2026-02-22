using System;

namespace GestionProjet.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateEcheance { get; set; }
        public int ProjetId { get; set; }
        public int StatutId { get; set; }
        public int? PrioriteId { get; set; }
        public int? AssigneeId { get; set; }

        // Propriétés de navigation
        public Projet Projet { get; set; }
        public Statut Statut { get; set; }
        public Priorite Priorite { get; set; }
        public Utilisateur Assignee { get; set; }

        public Tache()
        {
            DateCreation = DateTime.Now;
        }
    }
}
