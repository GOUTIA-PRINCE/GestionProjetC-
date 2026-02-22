using System.Collections.Generic;
using GestionProjet.Models;

namespace GestionProjet.Repositories
{
    public interface ITacheRepository
    {
        List<Tache> GetAllByProjet(int projetId);
        List<Tache> GetAllByAssignee(int assigneeId);
        Tache GetById(int id);
        void Add(Tache tache);
        void Update(Tache tache);
        void Delete(int id);
        void UpdateStatut(int tacheId, int nouveauStatutId);
        
        List<Statut> GetStatuts();
        List<Priorite> GetPriorites();
    }
}
