using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Models.Identity
{
    public class EditUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string RedirectPath { get; set; }
        public string Password { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public List<string> AccessRoles { get; set; }
    }
}