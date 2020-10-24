using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Models.Repository;
using System.Models.Work;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace System.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        AppRepository repo;
        public ReportController()
        {
            repo = new AppRepository();
        }
        // GET: Report
        public ActionResult Reports()
        {
            return View();
        }
      
          }
}