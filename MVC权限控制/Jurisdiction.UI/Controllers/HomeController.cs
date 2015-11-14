using Jurisdiction.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jurisdiction.UI.Controllers
{
    public class HomeController : Controller
    {
        IBLL.UsersIBLL userbll = null;
        public HomeController(IBLL.UsersIBLL ubll)
        {
            userbll = ubll;
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {

            return View();
        }

    }
}
