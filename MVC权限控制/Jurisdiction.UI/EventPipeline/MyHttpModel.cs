using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHelper;

namespace Jurisdiction.UI.EventPipeline
{
    public class MyHttpModel:IHttpModule
    {
        public void Dispose()
        {
         
        }

        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += context_AuthorizeRequest;
        }

        void context_AuthorizeRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            //解密tike 并赋值到user
            WebHelper.MyFormsPrincipal<UserTick>.TrySetUserInfo(app.Context);
        }
    }
}