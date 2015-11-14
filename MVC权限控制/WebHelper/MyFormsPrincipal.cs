using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebHelper
{
     
//
// 返回结果:
//     当前 HTTP 请求的安全信息。
    public class MyFormsPrincipal<T>:IPrincipal where T :class,new()
    {

        public MyFormsPrincipal(FormsAuthenticationTicket  tike,T user)
        {
            _userData = user;
            _identity = new FormsIdentity(tike);
        }

       public IIdentity _identity;
       public T _userData;
       // 摘要:
       //     获取当前用户的标识。
       //
       // 返回结果:
       //     与当前用户关联的 System.Security.Principal.IIdentity 对象。
       public IIdentity Identity {
           get
           {
               return _identity;
           }
       }

      public bool IsInRole(string role){
          return true;
      }
       /// <summary>
       /// 加密写入cookie
       /// </summary>
       /// <param name="loginName">用户名</param>
       /// <param name="userData">用户数据</param>
       /// <param name="date">过期时间 默认分钟</param>
       public static  void SingIn(string loginName, T userData, int minute, bool isContinue)
       {
           if (string.IsNullOrEmpty(loginName))
           {
               throw new ArgumentNullException("loginName");
           }
           if (userData == null)
           {
               throw new ArgumentNullException("userData");
           }

           // 1. 把需要保存的用户数据转成一个字符串。
           string data = null;
           if (userData != null)
               data = (new JavaScriptSerializer()).Serialize(userData);


           // 2. 创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
           FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               2,loginName,DateTime.Now,DateTime.Now.AddMinutes(30),isContinue,data);

           // 3. 加密Ticket，变成一个加密的字符串。
           string cookieValue = FormsAuthentication.Encrypt(ticket);
           // 4. 根据加密结果创建登录Cookie
           HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
           cookie.HttpOnly = true;
           cookie.Secure = FormsAuthentication.RequireSSL;
           //cookie.Domain = FormsAuthentication.CookieDomain;
           cookie.Path = FormsAuthentication.FormsCookiePath;
           if (isContinue)
           {
               cookie.Expires = DateTime.Now.AddMinutes(minute);
           }

           HttpContext context = HttpContext.Current;
           if (context == null)
               throw new InvalidOperationException();

           // 5. 写登录Cookie
  
           context.Response.Cookies.Add(cookie);
       }


       /// <summary>
       /// 根据上下文设置user对象
       /// </summary>
       /// <param name="context"></param>
       public static void TrySetUserInfo(HttpContext context)
       {
           if (context==null)
           {
               throw new ArgumentException("null Context");
           }
           //获得加密的用户cookie信息
           var cookie=context.Request.Cookies[FormsAuthentication.FormsCookieName];
           if (cookie == null || string.IsNullOrEmpty(cookie.Value))
           {
               return;
           }
           //将信息解密为tike对象
           FormsAuthenticationTicket tike = FormsAuthentication.Decrypt(cookie.Value);
           if (tike == null)
           {
               return;
           }
          T userData=new JavaScriptSerializer().Deserialize<T>(tike.UserData);
          context.User =new MyFormsPrincipal<T>(tike,userData);

       }
    }
}
