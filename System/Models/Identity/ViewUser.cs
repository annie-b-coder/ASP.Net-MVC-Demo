using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Models.Identity
{
    public class ViewUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}