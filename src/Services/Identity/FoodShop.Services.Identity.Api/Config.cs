using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FoodShop.Services.Identity.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(name: "foodShop.user.read", displayName: "Read user"),
            new ApiScope(name: "foodShop.user.create", displayName: "Create user"),
            new ApiScope(name: "foodShop.user.delete", displayName: "Delete user"),
            new ApiScope(name: "foodShop.user.update", displayName: "Update user"),
            new ApiScope(name: "foodShop.product.read", displayName: "Read product"),
            new ApiScope(name: "foodShop.product.create", displayName: "Create product"),
            new ApiScope(name: "foodShop.product.delete", displayName: "Delete product"),
            new ApiScope(name: "foodShop.product.update", displayName: "Update product"),
            new ApiScope(name: "foodShop.coupon.read", displayName: "Read coupon"),
            new ApiScope(name: "foodShop.coupon.create", displayName: "Create coupon"),
            new ApiScope(name: "foodShop.coupon.delete", displayName: "Delete coupon"),
            new ApiScope(name: "foodShop.coupon.update", displayName: "Update coupon"),
            new ApiScope(name: "foodShop.inventory.read", displayName: "Read inventory"),
            new ApiScope(name: "foodShop.inventory.create", displayName: "Create inventory"),
            new ApiScope(name: "foodShop.inventory.delete", displayName: "Delete inventory"),
            new ApiScope(name: "foodShop.inventory.update", displayName: "Update inventory"),
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "foodShop-User-Management-Admin",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:44363/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44363/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44363/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "foodShop.user.read",
                    "foodShop.user.create",
                    "foodShop.user.delete",
                    "foodShop.user.update",
                }
            },
            new Client
            {
                ClientId = "foodShop-Product-Management-Admin",
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:port/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:port/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:port/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "foodShop.product.read",
                    "foodShop.product.create",
                    "foodShop.product.delete",
                    "foodShop.product.update",
                }
            },
            new Client
            {
                ClientId = "foodShop-Coupon-Management-Admin",
                ClientSecrets = { new Secret("511536EF-0C79-F270-80CA-1C89C192F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:port/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:port/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:port/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "foodShop.coupon.read",
                    "foodShop.coupon.create",
                    "foodShop.coupon.delete",
                    "foodShop.coupon.update",
                }
            }, 
            new Client
            {
                ClientId = "foodShop-Inventory-Management-Admin",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4058-A3D6-1C89C192F69A".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:port/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:port/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:port/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "foodShop.inventory.read",
                    "foodShop.inventory.create",
                    "foodShop.inventory.delete",
                    "foodShop.inventory.update",
                }
            },
            new Client
            {
                ClientId = "foodShop-Customer",
                ClientSecrets = { new Secret("511536EF-A3D6-0C79-F270-A37998FB86B0".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:port/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:port/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:port/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "foodShop.user.read",
                    "foodShop.product.read",
                    "foodShop.coupon.read",
                    "foodShop.inventory.read",
                }
            }
        };
    }
}
