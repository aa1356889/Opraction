using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
namespace Jurisdiction.UI.App_Start
{
    public class AutofacRegister
    {
        //autofac容器
        public static IContainer autofacContainer;
        public static void Register()
        {
            //创建一个ioc容器
            var buid = new ContainerBuilder();
            Assembly ass = Assembly.Load("Jurisdiction.DAL");
            Assembly asbll = Assembly.Load("Jurisdiction.BLL");
            //已接口的形式注入ioc容器
            buid.RegisterAssemblyTypes(ass, asbll).AsImplementedInterfaces();
            buid.RegisterControllers(Assembly.GetExecutingAssembly());  //注入所有Controller
            var container = buid.Build();
            autofacContainer = container;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//普通的MVC Contr
        }

        /// <summary>
        /// 在IOC容器中获得指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resove<T>(){

           return autofacContainer.Resolve<T>();
        }
    }
}