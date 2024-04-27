using System.ComponentModel.DataAnnotations;

namespace BodyBank.Model
{
    public class CommandeOrgane
    {
        public int CommandeOrganeId { get; set; }

        [Required]
        public Organne Organne { get; set; }

        [Required]
        public Commande Commande { get; set; }

        public CommandeOrgane()
        {

        }
    }
}
