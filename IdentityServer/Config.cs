using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("bapi", "Boilerplate api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            string s = StandardScopes.OpenId.ToString() + " " + StandardScopes.Profile.ToString() + StandardScopes.Email.ToString();
            return new List<Client>
            {
                new Client
                {
                    ClientId = "bapiclient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = new List<string> {
                    "http://localhost:3000"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { StandardScopes.OpenId.ToString(), StandardScopes.Profile.ToString(), StandardScopes.Email.ToString(),
                        "bapi" },
                    AllowOfflineAccess = true
                }
            };
        }

    }
}