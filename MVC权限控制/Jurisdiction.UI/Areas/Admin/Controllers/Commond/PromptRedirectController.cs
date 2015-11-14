using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jurisdiction.UI.Areas.Admin.Controllers.Commond
{
    /// <summary>
    /// 后台公共的提示跳转类
    /// </summary>
    public class PromptRedirectController : Controller
    {
        //
        // GET: /Admin/PromptRedirect/

        public ActionResult Reirect()
        {
            return View();
        }

    }
}
