using BodyBank.Authentification;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BodyBank.Controllers
{
    public class CustomController : ControllerBase
    {
        protected bool IsAdmin()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                var roles = currentUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
                return roles.Contains(RolesUtilisateur.Administrateur);
            }
            return false;
        }

        protected string? GetUserName()
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Name))
                return currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return null;
        }
    }
}
