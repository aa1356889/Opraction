using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper;

namespace Jurisdiction.UI.Areas.Admin.WebBase
{
    /// <summary>
    /// 视图父类帮助类
    /// </summary>
    public class BaseHelper
    {
        public BaseHelper(UrlHelper url)
        {
            UrlHelper = url;
        }


        public UrlHelper UrlHelper { get; set; }

    }
}