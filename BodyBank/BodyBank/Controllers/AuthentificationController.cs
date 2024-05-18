using BodyBank.Authentification;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuthentication.NET6._0.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly UserManager<Utilisateur> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthentificationController(
            UserManager<Utilisateur> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var RolesUtilisateur = await _userManager.GetRolesAsync(user);
                
                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Name, user.UserName),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in RolesUtilisateur)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);


            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Utilisateur user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                Password = model.Password,
                SecurityStamp = Guid.NewGuid().ToString(),
                PrenomUtil = model.PrenomUtil,
                NomUtil = model.NomUtil,
                AdresseUtil = null
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(RolesUtilisateur.Utilisateur))
                await _roleManager.CreateAsync(new IdentityRole(RolesUtilisateur.Utilisateur));

            if (await _roleManager.RoleExistsAsync(RolesUtilisateur.Utilisateur))
            {
                await _userManager.AddToRoleAsync(user, RolesUtilisateur.Utilisateur);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Utilisateur user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                Password = model.Password,
                SecurityStamp = Guid.NewGuid().ToString(),
                PrenomUtil = model.PrenomUtil,
                NomUtil = model.NomUtil,
                AdresseUtil = null
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(RolesUtilisateur.Administrateur))
                await _roleManager.CreateAsync(new IdentityRole(RolesUtilisateur.Administrateur));
            if (!await _roleManager.RoleExistsAsync(RolesUtilisateur.Utilisateur))
                await _roleManager.CreateAsync(new IdentityRole(RolesUtilisateur.Utilisateur));

            if (await _roleManager.RoleExistsAsync(RolesUtilisateur.Administrateur))
            {
                await _userManager.AddToRoleAsync(user, RolesUtilisateur.Administrateur);
            }
            if (await _roleManager.RoleExistsAsync(RolesUtilisateur.Utilisateur))
            {
                await _userManager.AddToRoleAsync(user, RolesUtilisateur.Utilisateur);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
