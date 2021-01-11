using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PromoCodeProject.Common;
using PromoCodeProject.Models;
using PromoCodeProject.Models.Options;

namespace PromoCodeProject.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class JwtTokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomTokenOptions _tokenOptions;
        private readonly ApplicationDbContext _identityStore;
        private readonly IConfiguration _configuration;
        public JwtTokenController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager, ApplicationDbContext store, ICustomTokenOptions tokenOptions, IConfiguration configuration)
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _tokenOptions = tokenOptions;
            _identityStore = store;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Generate([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(MessagesConstants.TokenNotGenerated);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var applicationUsers = _identityStore.Users.FirstOrDefault(u => u.Email == model.Email);
                if (applicationUsers == null) return BadRequest(MessagesConstants.TokenNotGenerated);
                user = applicationUsers;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return BadRequest(MessagesConstants.TokenNotGenerated);

            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["CustomTokenOptions:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Issuer,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            GenericResponse response = new GenericResponse()
            {
                Data = new JwtSecurityTokenHandler().WriteToken(token),
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Message = MessagesConstants.TokenGenerated
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("SignUpUser")]
        public async Task<IActionResult> SignUpUser([FromBody] SignUpViewModel model)
        {
            GenericResponse response = new GenericResponse();

            var userExists = _identityStore.Users.FirstOrDefault(b => b.Email == model.Email.ToLower());
            if (userExists == null)
            {
                var appUser = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpper()
                };
                var userResult = _userManager.CreateAsync(appUser, model.Password).GetAwaiter().GetResult();
                if (userResult.Succeeded)
                {
                    if (!_identityStore.UserClaims.Any(cc =>
                        cc.ClaimType == ClaimTypeConstants.Employee && cc.ClaimValue == model.Email && cc.UserId == appUser.Id))
                    {
                        _identityStore.UserClaims.Add(new IdentityUserClaim<string>()
                        {
                            UserId = appUser.Id,
                            ClaimType = ClaimTypeConstants.Employee,
                            ClaimValue = model.Email
                        });

                        _identityStore.SaveChanges();
                        response = new GenericResponse()
                        {
                            Data = appUser,
                            Success = true,
                            HttpStatusCode = HttpStatusCode.OK,
                            Message = MessagesConstants.SignedUp
                        };
                    }
                }
            }
            else
            {
                response = new GenericResponse()
                {
                    Success = true,
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = MessagesConstants.UserExists
                };
            }
            return Ok(response);
        }
    }
}