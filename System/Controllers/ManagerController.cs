using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.Repository;
using System.Web;
using System.Web.Mvc;

namespace System.Controllers
{
    public class ManagerController : Controller
    {
        UserRepository repo;
       
        public ManagerController()
        {
            repo = new UserRepository();
        }
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }


    }
}