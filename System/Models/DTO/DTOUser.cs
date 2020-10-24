using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Models.DTO
{
    public class DTOUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string RedirectPath { get; set; }
        public List<DTORole> Roles { get; set; }
    }
}