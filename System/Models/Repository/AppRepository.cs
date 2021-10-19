using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Models.Repository
{
    public class AppRepository
    {
        private AppContext db = null;

        private bool disposed = false;

        public AppRepository()
        {
            db = new AppContext();
        }

        private UserRepository userRepository;        

        public UserRepository userRepo
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }       
    }
}