using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAppliccation.Models
{
    public class AuthorizationFilters : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuth = context.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
