using BodyBank.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BodyBank.Authentification
{
    public class Utilisateur : IdentityUser
    {
        [Required]
        public string PrenomUtil { get; set; }
        [Required]
        public string NomUtil { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "L'adresse courriel est requise")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        public string? Password { get; set; }

        public Addresse? AdresseUtil { get; set; }

        public Utilisateur() { }
    }
}
