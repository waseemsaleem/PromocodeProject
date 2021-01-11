using System.Linq;
using Microsoft.AspNetCore.Identity;
using PromoCodeProject.Models.Options;

namespace PromoCodeProject.Models.Seedings
{
    public class SeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CustomTokenOptions _tokenOptions;
        public SeedData(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager, CustomTokenOptions tokenOptions)
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _tokenOptions = tokenOptions;
        }
        public SeedData()
        {

        }
        public void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            var testBlog = context.Users.FirstOrDefault(b => b.UserName == "waseem");
            if (testBlog == null)
            {
                context.Users.Add(new ApplicationUser() { UserName = "waseem" });
            }
            var appUser = new ApplicationUser()
            {
                UserName = "waseem",
                Email = "waseembt10029@hotmail.com"
            };
            var userResult = _userManager.CreateAsync(appUser, "waseem").GetAwaiter().GetResult();
            if (userResult.Succeeded)
            {
                if (!context.UserClaims.Any(cc => cc.ClaimType == ClaimTypeConstants.Employee && cc.ClaimValue == "waseem" && cc.UserId == appUser.Id))
                {
                    context.UserClaims.Add(new IdentityUserClaim<string>()
                    {
                        UserId = appUser.Id,
                        ClaimType = ClaimTypeConstants.Employee,
                        ClaimValue = "waseem"
                    });

                    context.SaveChanges();

                    // TODO: log result of adding claim
                }

            }
        }
    }
}