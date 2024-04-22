using System.ComponentModel.DataAnnotations;

namespace BodyBank.Model
{
    public class CommandeOrgane
    {
        public int IDCommandeOrgane { get; set; }

        [Required]
        public Organ Organ { get; set; }

        [Required]
        public Commande Commande { get; set; }

        public CommandeOrgane()
        {

        }
    }
}
