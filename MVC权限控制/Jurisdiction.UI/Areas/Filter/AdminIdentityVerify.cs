using Jurisdiction.UI.Areas.Filter.Attrbute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace Jurisdiction.UI.Areas.Filter
{
    
    /// <summary>
    /// 后台身份验证
    /// </summary>
    public class AdminIdentityVerifyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 在执行action方法之前调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断是否打了不进行登陆验证的特性
            Type nocheck = typeof(NoCheckLoginAttribute);
            if (filterContext.ActionDescriptor.IsDefined(nocheck, false) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(nocheck, false))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            //只做后台身份认证
            if (filterContext.RouteData.DataTokens["area"] != null && filterContext.RouteData.DataTokens["area"].ToString() == "Admin")
            {
              
                //判断用户是否有登陆
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    //获得cooike保存的用户信息转化为正确的数据
                    FormsIdentity identity = filterContext.HttpContext.User.Identity as FormsIdentity;
                }
                else
                {
                    ViewResult result = new ViewResult();
                    result.ViewData["url"] = "/admin/Login/Index";
                    result.ViewData["ShowHtml"] = "您还没有登陆呢.......";
                    result.ViewName = "/areas/admin/views/PromptRedirect/Reirect.cshtml";
                    //如果没有登陆跳转到登陆页面
                    filterContext.Result = result;
                }
            }


            base.OnActionExecuting(filterContext);
        }
    }
}