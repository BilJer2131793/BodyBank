using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace BodyBank.Models
{
    public class Addresse
    {

        public int AddresseId { get; set; }
        [Required]
        public int NoCivique { get; set; }
        [Required]
        public string Rue { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public string Province { get; set; }
        public Addresse() { }
    }
}
