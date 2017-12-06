using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;

namespace DotNetCoreAppliccation.Models
{
    public static class ClaimsExtension
    {
        public static void UpdateClaim(IPrincipal principal, string key, string value)
        {
            var identity = principal.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            // add new claim
            identity.AddClaim(new Claim(key, value));


            //var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
        }
        
        public static string GetClaimValue(IPrincipal principal, string key)
        {
            var identity = principal.Identity as ClaimsIdentity;            
            if (identity == null)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            if (claim != null)
                return claim.Value;
            else
                return string.Empty;
        }
    }   

}
