using System.Windows.Forms;

namespace GestionProjet.Views
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }
        
        public void SetStats(int nbProjets, int nbTaches)
        {
            lblProjets.Text = $"Projets Actifs : {nbProjets}";
            lblTaches.Text = $"Mes Tâches : {nbTaches}";
        }
    }
}
