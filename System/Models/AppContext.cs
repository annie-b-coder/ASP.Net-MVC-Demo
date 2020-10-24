using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Models.Identity;
using System.Web;

namespace System.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}