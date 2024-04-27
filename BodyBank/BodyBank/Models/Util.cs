using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BodyBank.Models;
using Microsoft.EntityFrameworkCore;
namespace BodyBank.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class Util
    {

        public int UtilId { get; set; }
        [Required]
        public string PrenomUtil { get; set; }
        [Required]
        public string NomUtil { get; set; }
        [Required]
        public string Email { get; set; }
        public Addresse? AdresseUtil { get; set; }

        public Util()
        {
        }
    }
}
