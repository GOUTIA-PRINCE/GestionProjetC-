//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using GestionProjet.Views;

//namespace GestionProjet
//{
//    internal static class Program
//    {
//        /// <summary>
//        /// Point d'entrée principal de l'application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new ProjetView());
//        }
//    }
//}

using System;
using System.Windows.Forms;
using GestionProjet.Views;
using GestionProjet.Controllers;

namespace GestionProjet
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Démarrer avec le formulaire de login
            var loginForm = new LoginForm();
            var loginController = new LoginController(loginForm);

            Application.Run(loginForm);
        }
    }
}