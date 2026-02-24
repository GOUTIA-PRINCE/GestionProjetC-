using System;
using System.Windows.Forms;
using GestionProjet.Controllers;

namespace GestionProjet.Views
{
    public partial class DashboardForm : Form
    {
        private MainController _controller;

        public DashboardForm()
        {
            InitializeComponent();
            SetupInteractivity();
        }

        public void SetController(MainController controller)
        {
            _controller = controller;
        }

        private void SetupInteractivity()
        {
            pnlProjets.Cursor = Cursors.Hand;
            pnlTaches.Cursor = Cursors.Hand;

            pnlProjets.Click += (s, e) => _controller?.OuvrirGestionProjets();
            lblProjetsCount.Click += (s, e) => _controller?.OuvrirGestionProjets();
            lblProjetsTitle.Click += (s, e) => _controller?.OuvrirGestionProjets();

            pnlTaches.Click += (s, e) => _controller?.OuvrirMesTaches();
            lblTachesCount.Click += (s, e) => _controller?.OuvrirMesTaches();
            lblTachesTitle.Click += (s, e) => _controller?.OuvrirMesTaches();
        }
        
        public void SetStats(int nbProjets, int nbTaches)
        {
            lblProjetsCount.Text = nbProjets.ToString();
            lblTachesCount.Text = nbTaches.ToString();
        }
    }
}
