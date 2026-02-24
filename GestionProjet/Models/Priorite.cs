// =============================================================================
// Modèle Priorité
// Représente la priorité d'une tâche (ex : Faible, Normale, Haute, Urgente).
// Chaque priorité possède une couleur hexadécimale utilisée pour
// le code couleur visuel dans le Kanban.
// =============================================================================

namespace GestionProjet.Models
{
    /// <summary>
    /// Modèle représentant le niveau de priorité d'une tâche.
    /// La couleur hexadécimale est utilisée pour afficher une barre colorée
    /// sur chaque carte de tâche dans le tableau Kanban.
    /// </summary>
    public class Priorite
    {
        /// <summary>Identifiant unique de la priorité (clé primaire en base).</summary>
        public int Id { get; set; }

        /// <summary>Libellé de la priorité (ex : "Faible", "Normale", "Haute", "Urgente").</summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Code couleur hexadécimal associé à cette priorité (ex : "#FF0000" pour rouge).
        /// Utilisé pour colorier la bordure des cartes Tâche dans le Kanban.
        /// </summary>
        public string CouleurHex { get; set; }

        /// <summary>
        /// Retourne le libellé de la priorité, utilisé dans les ComboBox et DataGridView.
        /// </summary>
        /// <returns>Le libellé de la priorité.</returns>
        public override string ToString()
        {
            return Libelle;
        }
    }
}
