using Jurisdiction.UI.Areas.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper;

namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    /// <summary>
    ///工作台控制器
    /// </summary>
    public class WorkbenchController :MyControllers
    {
        //
        // GET: /Admin/Workbench/

        public ActionResult Index()
        {
            MyFormsPrincipal<UserTick> ut = userTick;
            var aa = ut._userData.Uanme;
            return View();
        }

    }
}
