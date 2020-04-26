using BusinessLogic.Models;
using BusinessLogic.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserService userService;
        private readonly ISecurityService securityService;

        public ResourceOwnerPasswordValidator(IUserService userService, ISecurityService securityService)
        {
            this.userService = userService;
            this.securityService = securityService;
        }

        //this is used to validate user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await userService.GetOneAsync(context.UserName);
                if (user != null)
                {
                    if (securityService.CheckPassword(context.Password, user.Password))
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.ID.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        public static Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.ID.ToString() ?? ""),
            new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName)) ? (user.FirstName + " " + user.LastName) : ""),
            new Claim(JwtClaimTypes.Email, user.Email  ?? ""),

            new Claim(JwtClaimTypes.Role, user.Role.ToString())
            };
        }
    }
}
