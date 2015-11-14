using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using Autofac.Integration.Mvc;
using Jurisdiction.UI.App_Start;
namespace Jurisdiction.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacRegister.Register();
            ////创建一个ioc容器
            //var buid = new ContainerBuilder();
            //Assembly ass = Assembly.Load("Jurisdiction.DAL");
            //Assembly asbll = Assembly.Load("Jurisdiction.BLL");
            ////已接口的形式注入ioc容器
            //buid.RegisterAssemblyTypes(ass, asbll).AsImplementedInterfaces();
            //buid.RegisterControllers(Assembly.GetExecutingAssembly());  //注入所有Controller
            //var container = buid.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//普通的MVC Contr
            
        }
    }
}