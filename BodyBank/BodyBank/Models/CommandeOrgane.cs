using System.ComponentModel.DataAnnotations;

namespace BodyBank.Model
{
    public class CommandeOrgane
    {
        public int CommandeOrganeId { get; set; }

        [Required]
        public Organne Organ { get; set; }

        [Required]
        public Commande Commande { get; set; }

        public CommandeOrgane()
        {

        }
    }
}
