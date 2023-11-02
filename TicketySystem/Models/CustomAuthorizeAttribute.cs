using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketySystem.Models
{
    public class CustomAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly string _requiredRole;

        public CustomAuthorizeAttribute(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string userRole = filterContext.HttpContext.Session["userRole"].ToString();

            if (userRole != _requiredRole)
            {
                filterContext.Result = new HttpUnauthorizedResult(); // Unauthorized access
            }
        }
    }

}