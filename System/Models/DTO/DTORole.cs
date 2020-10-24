using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Models.DTO
{
    public class DTORole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DTOUser> Users { get; set; }
    }
}