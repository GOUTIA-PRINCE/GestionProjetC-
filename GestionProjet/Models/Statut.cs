// =============================================================================
// Modèle Statut
// Représente le statut d'une tâche dans le tableau Kanban.
// Les statuts sont ordonnés et correspondent aux colonnes du Kanban
// (ex : "À faire", "En cours", "Terminé").
// =============================================================================

namespace GestionProjet.Models
{
    /// <summary>
    /// Modèle représentant le statut d'une tâche.
    /// Les statuts définissent les colonnes du tableau Kanban et sont triés
    /// par leur propriété <see cref="Ordre"/>.
    /// </summary>
    public class Statut
    {
        /// <summary>Identifiant unique du statut (clé primaire en base).</summary>
        public int Id { get; set; }

        /// <summary>Libellé affiché du statut (ex : "À faire", "En cours", "Terminé").</summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Numéro d'ordre définissant la position de la colonne dans le Kanban
        /// (plus petit = plus à gauche).
        /// </summary>
        public int Ordre { get; set; }

        /// <summary>
        /// Retourne le libellé du statut, utilisé notamment pour afficher
        /// la valeur dans les ComboBox et DataGridView.
        /// </summary>
        /// <returns>Le libellé du statut.</returns>
        public override string ToString()
        {
            return Libelle;
        }
    }
}
