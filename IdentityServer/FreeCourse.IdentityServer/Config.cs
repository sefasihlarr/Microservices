// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
    {
        new ApiResource("resource_catalog")
        {
            Scopes = { "catalog_fullpermisssion", "photo_stock_fullpermisssion" }
        },


        new ApiResource("resource_basket")
        {
            Scopes = { "basket_fullpermisssion" }
        },

        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
{
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource
    {
        Name = "roles",
        DisplayName = "Kullanıcı rolleri",
        UserClaims = new[] { "role" }
    }
};




        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("catalog_fullpermisssion", "Catalog API"),
        new ApiScope("photo_stock_fullpermisssion", "Photo Stock API"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName),

        new ApiScope("basket_fullpermisssion", "Basket API için erişim "),
    };

        public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientName = "Asp.net Core MVC",
            ClientId = "WebMvcClient",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes  =GrantTypes.ClientCredentials,
            AllowedScopes = { "catalog_fullpermisssion", "photo_stock_fullpermisssion", IdentityServerConstants.LocalApi.ScopeName }
        },

         new Client
        {
            ClientName = "Asp.net Core MVC",
            ClientId = "WebMvcClientForUser",
            AllowOfflineAccess = true,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes  =GrantTypes.ResourceOwnerPassword,
            AllowedScopes = { "basket_fullpermisssion" , IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName, "roles" },
            AccessTokenLifetime = 1*60*60,
            RefreshTokenExpiration = TokenExpiration.Absolute,
            AbsoluteRefreshTokenLifetime =(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
            RefreshTokenUsage = TokenUsage.ReUse

        },
    };
    }

}