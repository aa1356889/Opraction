
using Jurisdiction.Entity.Extend;
using Jurisdiction.Entity;
using Jurisdiction.UI.Areas.Filter.Attrbute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebHelper;
using Jurisdiction.EntityView;
using Jurisdiction.Extend;

namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    [NoCheckLogin,NochekOpraction]
    public class LoginController:MyControllers
    {
        IBLL.UsersIBLL userbll = null;
        //通过构造函数注入需要使用的业务类
        public LoginController(IBLL.UsersIBLL bll)
        {
            userbll = bll;
          
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 处理登陆请求
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessLogin(string loginName, string passWord, string isPersistence)
        {
           
          
            if (string.IsNullOrEmpty(loginName))
            {
                return ProcessNo("用户名不能为空");
            }
            if (string.IsNullOrEmpty(passWord))
            {
                return ProcessNo("密码不能为空");
            }
            passWord=Kite.Md5Entry(passWord).ToLower();
            Jurisdiction.Entity.Users user = userbll.Query(c => c.LoginName == loginName && c.UpassWord == passWord).ToList<Jurisdiction.Entity.Users>().FirstOrDefault();
            if (user == null)
            {
                return ProcessNo("用户名或密码错误");
            }else
            {
                bool index = !string.IsNullOrEmpty(isPersistence);
                //查询用户权限
                List<OpractionsExtend> opraction =base.Opracitonbll.GetOpractionByUid(user.uid);
                //将权限写入缓存
                CaheManager.SetData(user.LoginName, opraction,30);
                //加密写入cookie
                MyFormsPrincipal<UserTick>.SingIn(user.LoginName, new UserTick() { Uanme = user.Uname,LoginName=user.LoginName,UId = user.uid }, 30, index);
                return ProcessOk("登陆成功", Url.Action("Index", "Workbench", new  {area="Admin" }));
            }
        }

        /// <summary>
        ///处理退出登陆请求
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            //清除cookie保存的用户信息
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now.AddDays(-1000);
            Response.Cookies.Add(cookie);
            ViewData["url"] = "/Admin/Login/Index";
            ViewData["ShowHtml"] = "退出登陆成功正在跳转登陆页面.......";
            return View("/areas/admin/views/PromptRedirect/Reirect.cshtml");
        }


        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            return View();
        }

        

    }
}
