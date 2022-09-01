// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TubimProject.AuthenticationServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                    new ApiScope (IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(), //subId
                new IdentityResources.Profile(), /// 
                new IdentityResource(){ Name="Roles",DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new [] { "role"} }
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>(){

              new Client()
                 {
                     ClientId = "TubimProject_ESNNBRN29062022",
                    ClientName="Tubim Projesi UI Katmanı",
                   ClientSecrets=new[] {new Secret("Bim05*!3240__*".Sha256())},
                   AllowedGrantTypes= GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                   AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,"Roles",IdentityServerConstants.LocalApi.ScopeName},
                   AccessTokenLifetime=2*60*60,
                   AllowOfflineAccess=true,
                   RefreshTokenUsage=TokenUsage.ReUse,
                   RefreshTokenExpiration=TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
        },
    };
        }
    }
}