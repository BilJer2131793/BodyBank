using System.ComponentModel.DataAnnotations;
namespace BodyBank.Authentification
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        public string? Password { get; set; }
    }
}
