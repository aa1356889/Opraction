
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
using System.IO;
using System.Drawing;
using Redis;

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
            RedisManager.Lpush("text", "哈哈");
            return View();
        }

        /// <summary>
        /// 处理登陆请求
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessLogin(string loginName, string passWord, string isPersistence, string vcode)
        {
            var aa = RedisManager.LPop<string>("text");
          
            if (string.IsNullOrEmpty(loginName))
            {
                return ProcessNo("用户名不能为空");
            }
            if (string.IsNullOrEmpty(passWord))
            {
                return ProcessNo("密码不能为空");
            }
            if (string.IsNullOrEmpty(vcode))
            {
                return ProcessNo("验证码不能为空");
            }
            var code = Session[keyS.Vicode].ToString();
            if (vcode.ToLower() != code.ToLower())
            {
                return ProcessNo("验证码输入错误");
            }
            passWord=Kite.Md5Entry(passWord).ToLower();
            Jurisdiction.Entity.Users user = userbll.Query(c => c.LoginName == loginName && c.UpassWord == passWord&&c.IsDelete==0).ToList<Jurisdiction.Entity.Users>().FirstOrDefault();
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
        /// 获取验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {

            return File(CreateCheckCodeImage(GenerateCheckCode()).ToArray(), "image/gif");
        }


        #region 私有

        //产生验证码
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            while (checkCode.Length < 4)
            {
                number = random.Next();

                if (number % 2 == 0)
                {
                    code = (char)('0' + (char)(number % 10));
                    if (code == '0')
                    {
                        //如果是数字0，为了避免与字母o混淆，跳过
                        continue;
                    }
                }
                else
                {
                    code = (char)('A' + (char)(number % 26));
                    if (code == 'O')
                    {
                        continue;
                    }

                }

                checkCode += code.ToString();
            }

           ControllerContext.HttpContext.Session[keyS.Vicode] = checkCode;


            return checkCode;
        }

        //产生验证图片
        private MemoryStream CreateCheckCodeImage(string checkCode)
        {
            System.IO.MemoryStream ms = new MemoryStream();
            if (checkCode == null || checkCode.Trim() == String.Empty)
            {
                return ms;
            }

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 15.5)), 26);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 14, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms;
                //Response.ClearContent();
                //Response.ContentType = "image/Gif";
                //Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

        

    }
}
