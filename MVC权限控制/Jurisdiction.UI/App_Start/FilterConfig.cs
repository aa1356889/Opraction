using Jurisdiction.UI.Areas.Filter;
using System.Web;
using System.Web.Mvc;

namespace Jurisdiction.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //注册全局的登陆验证过滤器
            filters.Add(new AdminIdentityVerifyAttribute());
            ////注册全局的权限验证过滤器
            filters.Add(new OperactionAttribute());
        }
    }
}