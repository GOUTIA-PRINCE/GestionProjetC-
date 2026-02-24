// =============================================================================
// Point d'entrée principal de l'application GestionProjet
// Ce fichier démarre l'application Windows Forms en affichant d'abord
// le formulaire de connexion (LoginForm) associé à son contrôleur.
// =============================================================================

using System;
using System.Windows.Forms;
using GestionProjet.Views;
using GestionProjet.Controllers;

namespace GestionProjet
{
    /// <summary>
    /// Classe statique contenant le point d'entrée principal de l'application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// Active les styles visuels Windows, puis lance la boucle d'événements
        /// sur le formulaire de connexion.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Active les styles visuels modernes de Windows (thème Windows)
            Application.EnableVisualStyles();

            // Désactive le rendu de texte compatible GDI+ pour utiliser GDI à la place
            Application.SetCompatibleTextRenderingDefault(false);

            // Créer le formulaire de connexion (vue)
            var loginForm = new LoginForm();

            // Créer le contrôleur associé au formulaire de connexion
            // Le contrôleur s'enregistre automatiquement auprès de la vue
            var loginController = new LoginController(loginForm);

            // Démarrer la boucle d'événements Windows sur le formulaire de login
            Application.Run(loginForm);
        }
    }
}