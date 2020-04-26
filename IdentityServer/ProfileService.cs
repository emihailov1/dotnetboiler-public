using BusinessLogic.Services;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService userService;

        public ProfileService(IUserService userService)
        {
            this.userService = userService;
        }

        //connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    var user = await userService.GetOneAsync(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else
                {
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                    {
                        var user = await userService.GetOneAsync(int.Parse(userId.Value));

                        if (user != null)
                        {
                            context.IssuedClaims = ResourceOwnerPasswordValidator.GetUserClaims(user).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User profile not found");
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                {
                    var user = await userService.GetOneAsync(int.Parse(userId.Value));
                    if (user != null)
                    {
                        context.IsActive = user.Status == BusinessLogic.Enums.Status.Active ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User is not active");
            }
        }
    }
}
