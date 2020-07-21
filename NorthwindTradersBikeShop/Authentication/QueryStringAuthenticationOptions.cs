// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
//
using Microsoft.AspNetCore.Authentication;

namespace Northwind.BikeShop.Authentication
{
    class QueryStringAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string UsernameParameterName { get; set; } = "username";

        public string GroupsParameterName { get; set; } = "groups";
    }
}
