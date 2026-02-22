using System.Windows.Forms;

namespace GestionProjet.Controllers
{
    public abstract class BaseController
    {
        protected Form CurrentForm { get; set; }

        protected void NaviguerVers(Form form)
        {
            form.Show();
            CurrentForm?.Hide();
        }

        protected void AfficherMessage(string message, string titre = "Information")
        {
            MessageBox.Show(message, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected void AfficherErreur(string message, string titre = "Erreur")
        {
            MessageBox.Show(message, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected bool ConfirmerAction(string message)
        {
            return MessageBox.Show(message, "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}