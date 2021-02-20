using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using student_management_asp_uppgift1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace student_management_asp_uppgift1.Data
{
    public class ApplicationUserClaims : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaims(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("DisplayName", user.DisplayName));
            identity.AddClaim(new Claim(ClaimTypes.GivenName , user.FirstName));

            return identity;
        }
    }
}
