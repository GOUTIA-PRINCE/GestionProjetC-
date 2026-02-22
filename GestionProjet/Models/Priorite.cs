namespace GestionProjet.Models
{
    public class Priorite
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string CouleurHex { get; set; }

        public override string ToString()
        {
            return Libelle;
        }
    }
}
