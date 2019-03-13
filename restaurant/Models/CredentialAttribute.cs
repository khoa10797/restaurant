using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace restaurant.Models
{
    public class CredentialAttribute : AuthorizeAttribute
    {
        public string roleID { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (User)HttpContext.Current.Session["user"];
            List<string> privilegeLevels = (List<string>)HttpContext.Current.Session["role"];
            if (privilegeLevels == null)
                return false;
            if (session.groupID == "Admin")
                return true;
            return privilegeLevels.Contains(roleID);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}