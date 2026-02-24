// =============================================================================
// Vue - Formulaire d'inscription (RegisterForm)
// Affiche le formulaire de création de compte avec les champs nom, email,
// mot de passe et confirmation de mot de passe.
// La confirmation des mots de passe est vérifiée côté vue avant d'appeler
// le contrôleur, pour un feedback immédiat à l'utilisateur.
// =============================================================================

using System;
using System.Drawing;
using System.Windows.Forms;
using GestionProjet.Controllers;

namespace GestionProjet.Views
{
    /// <summary>
    /// Formulaire d'inscription (création de compte) de l'application.
    /// Vue passive (MVC) qui délègue la logique métier au <see cref="RegisterController"/>.
    /// La seule validation côté vue est la correspondance des mots de passe,
    /// pour offrir un feedback immédiat sans requête base de données.
    /// </summary>
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// Référence vers le contrôleur associé à cette vue.
        /// Injectée via <see cref="SetController"/>.
        /// </summary>
        private RegisterController _controller;

        /// <summary>
        /// Initialise le formulaire et active le glisser-déposer de la fenêtre.
        /// </summary>
        public RegisterForm()
        {
            InitializeComponent();
            // Active le déplacement personnalisé (la fenêtre est sans bordure)
            SetupDragging();
        }

        /// <summary>
        /// Active le déplacement de la fenêtre sans bordure par drag-and-drop
        /// sur le panneau d'en-tête (pnlHeader).
        /// </summary>
        private void SetupDragging()
        {
            Point lastLocation = Point.Empty;
            bool isMouseDown = false;

            pnlHeader.MouseDown += (s, e) => {
                isMouseDown = true;
                lastLocation = e.Location;
            };

            pnlHeader.MouseMove += (s, e) => {
                if (isMouseDown)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X,
                        (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            };

            pnlHeader.MouseUp += (s, e) => isMouseDown = false;
        }

        /// <summary>
        /// Enregistre le contrôleur associé à cette vue.
        /// Appelé par le <see cref="RegisterController"/> au moment de sa création.
        /// </summary>
        /// <param name="controller">Le contrôleur d'inscription.</param>
        public void SetController(RegisterController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "S'inscrire".
        /// Vérifie d'abord que les deux mots de passe sont identiques (validation côté vue).
        /// Si valide, délègue la création du compte au contrôleur.
        /// </summary>
        private void btnSInscrire_Click(object sender, EventArgs e)
        {
            // Validation rapide côté vue : les mots de passe doivent correspondre
            if (txtMotDePasse.Text != txtConfirmerMotDePasse.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Délégation au contrôleur pour les validations métier et la persistance
            _controller.Inscrire(txtNom.Text.Trim(), txtEmail.Text.Trim(), txtMotDePasse.Text);
        }

        /// <summary>
        /// Déclenché au clic sur le bouton "Retour".
        /// Retourne au formulaire de connexion via le contrôleur.
        /// </summary>
        private void btnRetour_Click(object sender, EventArgs e)
        {
            _controller.RetourLogin();
        }
    }
}
