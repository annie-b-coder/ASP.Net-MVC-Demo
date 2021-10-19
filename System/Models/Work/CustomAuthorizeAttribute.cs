using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.Repository;
using System.Web;
using System.Web.Mvc;

namespace System.Models.Work
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] allowedUsers = new string[] { };
        private string[] allowedRoles = new string[] { };

        public CustomAuthorizeAttribute()
        { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (!String.IsNullOrEmpty(base.Users))
            {
                allowedUsers = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < allowedUsers.Length; i++)
                {
                    allowedUsers[i] = allowedUsers[i].Trim();
                }
            }
            if (!String.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    allowedRoles[i] = allowedRoles[i].Trim();
                }
            }



            return httpContext.Request.IsAuthenticated &&
                 User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsers.Length > 0)
            {
                return allowedUsers.Contains(httpContext.User.Identity.Name);
            }
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (allowedRoles.Length > 0)
            {
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (IsInRole(httpContext.User.Identity.Name, allowedRoles[i]))
                        return true;
                }
                return false;
            }
            return true;
        }

        private bool IsInRole(string username, string role)
        {
            UserRepository repo = new UserRepository();
            var rolename = repo.GetUser(username).Roles.Select(z => z.Name).Contains(role);

            if (rolename) return true;
            else return false;
        }
    }
}