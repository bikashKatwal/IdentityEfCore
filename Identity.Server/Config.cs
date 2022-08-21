using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Server
{
    internal static class Config
    {
        internal static IEnumerable<IdentityResource> IdentityResources = new List<IdentityResource> {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource{
                   Name="rc.scope",
                   UserClaims=
                   {
                        "Identity.Admin"
                   }
               }
        };

        internal static IEnumerable<ApiResource> ApiResources = new List<ApiResource> {
              new ApiResource("ApiOne")
                {
                    Scopes = { "ApiOne"},
                    //UserClaims=new[]{ "Identity.Api.Admin" }
                },
                new ApiResource("ApiTwo")
                {
                    Scopes = { "ApiTwo" },
                    UserClaims=new[]{ "Identity.Api.Admin" }
                },
        };

        internal static IEnumerable<Client> Clients = new List<Client>{
                new Client
                {
                        ClientId = "client_id",
                        ClientSecrets = {new Secret("client_secret".ToSha256()) },
                        AllowedGrantTypes = GrantTypes.ClientCredentials,//Machine -To - Machine Communication
                        AllowedScopes = { "ApiOne" }
                },
                new Client
                {
                        ClientId = "client_id_mvc",
                        ClientSecrets = {new Secret("client_secret_mvc".ToSha256()) },
                        AllowedGrantTypes = GrantTypes.Code,
                        //RequirePkce = true,// GO THROUGH IT IN DETAILS
                                            //AllowedGrantTypes = GrantTypes.Implicit,
                        RedirectUris={ "https://localhost:44350/signin-oidc" },
                        PostLogoutRedirectUris={ "https://localhost:44350/Home/Index" },
                        AllowedScopes = {
                            "ApiOne",
                            "ApiTwo",
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "rc.scope"
                        },
                        RequireConsent=false ,
                        AllowOfflineAccess=true, //Allows offline access (refresh token)
                        //puts all claim in the id token
                        //AlwaysIncludeUserClaimsInIdToken=true,
                }
            };

        internal static IEnumerable<ApiScope> ApiScopes = new List<ApiScope> {
            new ApiScope{Name = "ApiOne"},
            new ApiScope{Name = "ApiTwo"}
        };
    }
}
