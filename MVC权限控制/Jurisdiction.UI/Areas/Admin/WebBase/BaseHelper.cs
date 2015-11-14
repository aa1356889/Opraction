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

        //js css 的资源文件夹
        string ResourceKey = "resorece";

        string ResourceNumKkey = "ResourceNumKkey";//资源版本号key
        /// <summary>
        /// URL方法对象
        /// </summary>
        public UrlHelper UrlHelper { get; set; }
        /// <summary>
        /// 加载资源路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string LoadResource(string path)
        {
            return UrlHelper.Content(ConfigHelper.LoadConfigAppction(ResourceKey)+"/"+path+"?v="+ConfigHelper.LoadConfigAppction(ResourceNumKkey));
        }

        public string LoadAreasResouce(string path)
        {
            return UrlHelper.Content("~/Areas" + path + "?v" + ConfigHelper.LoadConfigAppction(ResourceNumKkey));
        }
    }
}