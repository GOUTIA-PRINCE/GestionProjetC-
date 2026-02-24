// =============================================================================
// Contrôleur de base (BaseController) - Pattern MVC
// Classe abstraite dont héritent tous les contrôleurs de l'application.
// Fournit les méthodes communes de navigation entre les vues et d'affichage
// de boîtes de dialogue (messages, erreurs, confirmations).
// =============================================================================

using System.Windows.Forms;
using GestionProjet.Views;

namespace GestionProjet.Controllers
{
    /// <summary>
    /// Classe de base abstraite pour tous les contrôleurs de l'application.
    /// Implémente le pattern MVC en fournissant :
    /// - La navigation entre les vues (formulaires)
    /// - Des méthodes utilitaires pour l'affichage de messages
    /// </summary>
    public abstract class BaseController
    {
        /// <summary>
        /// Formulaire (vue) actuellement géré par ce contrôleur.
        /// Utilisé pour masquer la vue courante lors d'une navigation.
        /// </summary>
        protected Form CurrentForm { get; set; }

        /// <summary>
        /// Référence statique vers le formulaire principal (MainForm).
        /// Partagée entre tous les contrôleurs pour permettre le chargement
        /// de vues dans le panneau central de l'application (navigation SPA).
        /// </summary>
        public static MainForm MainView { get; set; }

        /// <summary>
        /// Navigue vers le formulaire spécifié.
        /// Si le MainForm est disponible, charge la vue dans son panneau central (mode SPA).
        /// Sinon, affiche le formulaire de manière classique et masque le formulaire courant.
        /// </summary>
        /// <param name="form">Le formulaire cible à afficher.</param>
        protected void NaviguerVers(Form form)
        {
            if (MainView != null && form != MainView)
            {
                // Mode SPA : charge la vue dans le panneau central du MainForm
                MainView.ChargerVue(form);
            }
            else
            {
                // Mode classique : affiche le nouveau formulaire et masque l'actuel
                form.Show();
                if (CurrentForm != null && CurrentForm != form)
                {
                    CurrentForm.Hide();
                }
            }
        }

        /// <summary>
        /// Affiche une boîte de dialogue d'information à l'utilisateur.
        /// </summary>
        /// <param name="message">Message à afficher.</param>
        /// <param name="titre">Titre de la boîte de dialogue (défaut : "Information").</param>
        protected void AfficherMessage(string message, string titre = "Information")
        {
            MessageBox.Show(message, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Affiche une boîte de dialogue d'erreur à l'utilisateur.
        /// </summary>
        /// <param name="message">Message d'erreur à afficher.</param>
        /// <param name="titre">Titre de la boîte de dialogue (défaut : "Erreur").</param>
        protected void AfficherErreur(string message, string titre = "Erreur")
        {
            MessageBox.Show(message, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Affiche une boîte de dialogue de confirmation Oui/Non.
        /// </summary>
        /// <param name="message">Question à poser à l'utilisateur.</param>
        /// <returns>true si l'utilisateur a cliqué sur "Oui", false sinon.</returns>
        protected bool ConfirmerAction(string message)
        {
            return MessageBox.Show(message, "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}