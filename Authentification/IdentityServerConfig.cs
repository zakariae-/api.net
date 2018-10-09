﻿using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;
using static IdentityServer4.IdentityServerConstants;

namespace Authentification
{
    public static class IdentityServerConfig
    {
        private const string ApiMainScope = "myScope";

        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    DisplayName = ApiMainScope,
                    Name = ApiMainScope,
                    Enabled = true,
                    UserClaims = new [] { JwtClaimTypes.Id, JwtClaimTypes.Email },
                    Scopes = new [] { new Scope(ApiMainScope), }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes = { ApiMainScope, StandardScopes.OfflineAccess },
                    AllowOfflineAccess = true,
                    AllowPlainTextPkce = false,
                    AllowedCorsOrigins = { "http://localhost:5000" },
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding
                }
            };
        }
    }
}
