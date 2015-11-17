using Jurisdiction.EntityView;
using Jurisdiction.Extend;
using Jurisdiction.UI.Areas.Admin.Controllers;
using Jurisdiction.UI.Areas.Filter.Attrbute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebHelper;
using WebHelper.Attrbute;

namespace Jurisdiction.UI.Areas.Filter
{
    /// <summary>
    /// 权限验证过滤器
    /// </summary>
    public class OperactionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 在进入action方法之前触发
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //打上了此特性则不进行权限验证
            Type tp = typeof(NochekOpraction);
            if (filterContext.ActionDescriptor.IsDefined(tp, false) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(tp, false))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            //只验证后台
            if (filterContext.RouteData.DataTokens["area"] != null && filterContext.RouteData.DataTokens["area"].ToString().ToLower() == "admin")
            {
                //获得当前控制器对象
                MyControllers controller = filterContext.Controller as MyControllers;
                //获得对应的url地址
                string areaName = filterContext.RouteData.DataTokens["area"].ToString();
                string action = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //获得重写验证url的特性对象
                object[] objes=filterContext.ActionDescriptor.GetCustomAttributes(typeof(InitValidateUrlAttribute),false);
                if (objes != null && objes.Length > 0)
                {
                    InitValidateUrlAttribute initurl = objes[0] as InitValidateUrlAttribute;
                    action =initurl.Action;
                }
                var list = controller.Opracton.Where(c => string.Compare(areaName, c.Mares, true) == 0 && string.Compare(controllerName, c.MContorll, true) == 0 && string.Compare(action, c.Action, true) == 0).ToList<OpractionsExtend>();
                //url不匹配表示没有权限
                if (list.Count<=0)
                {
                    //判断是ajax请求还是浏览器请求 响应不同结果
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        //ajax请求响应json格式字符串
                        JsonResult result= new JsonResult();
                        result.Data = new { State = 1, Message = "不好意思您没有权限哦,如需开通！请联系系统管理员" };
                        filterContext.Result = result;
                    }
                    else
                    {
                        ViewResult vresult = new ViewResult();
                        vresult.ViewData["url"] = controller.Url.Action("Index", "Workbench");
                        vresult.ViewData["ShowHtml"] = "不好意思您没有访问改页面的权限！如需开通请联系系统管理员.......";
                        vresult.ViewName = "/areas/admin/views/PromptRedirect/Reirect.cshtml";
                        //跳转到提示页面
                        filterContext.Result = vresult;

                    }
                }
               
            }
          
        }
    }
}