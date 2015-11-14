using Jurisdiction.Entity.Extend;
using Jurisdiction.EntityView;
using Jurisdiction.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebHelper;

namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// 自定义后台控制器公共父类。
    /// </summary>
    public class MyControllers : Controller
    {
        IBLL.OprationIBLL _opracitonbll = null;

        public IBLL.OprationIBLL Opracitonbll
        {
            get { return _opracitonbll; }
            set { _opracitonbll = value; }
        }

        public MyControllers()
        {
            _opracitonbll = Jurisdiction.UI.App_Start.AutofacRegister.Resove<IBLL.OprationIBLL>();
        }
        /// <summary>
        /// 当前登录用户基础信息
        /// </summary>
        public MyFormsPrincipal<UserTick> userTick
        {
            get
            {
                return HttpContext.User as MyFormsPrincipal<UserTick>;
            }
        }


        /// <summary>
        /// 当前用户所属权限
        /// </summary>
        public List<OpractionsExtend> Opracton {

            get
            {
                //先从缓存中拿
               List<OpractionsExtend> opractions=CaheManager.GetData<List<OpractionsExtend>>(userTick._userData.LoginName);
               if (opractions == null)
               {
                   //如果没有查询数据库
                   opractions=_opracitonbll.GetOpractionByUid(userTick._userData.UId);
                   //存入缓存
                   CaheManager.SetData(userTick._userData.UId.ToString(), opractions,30);
               }
               return opractions;
            }
        }

        /// <summary>
        /// 处理成功 返回处理信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessOk(string messsage)
        {
            return Content(new JavaScriptSerializer().Serialize(new { State = 0, Message = messsage }));
        }

        /// <summary>
        /// 处理失败返回处理信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult ProcessNo(string message)
        {
            return Content(new JavaScriptSerializer().Serialize(new { State = 1, Message = message }));
        }

        /// <summary>
        ///失败提示并跳转到一个新地址
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ActionResult ProcessNo(string message, string url)
        {
            return Content(new JavaScriptSerializer().Serialize(new { State = 1, Message = message, redirectUrl = url }));
        }

        /// <summary>
        ///成功提示并跳转到一个新地址
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ActionResult ProcessOk(string message, string url)
        {
            return Content(new JavaScriptSerializer().Serialize(new { State = 0, Message = message, redirectUrl = url }));
        }

        /// <summary>
        /// 没有权限返回处理信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult ProcessNoOperaction(string message)
        {
            return Content(new JavaScriptSerializer().Serialize(new { State = 2, Message = message }));
        }

        /// <summary>
        /// 处理成功返回数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessOkReData(object obj,string message="")
        {
            return Content(new JavaScriptSerializer().Serialize(new { State = 0, Message = message,Datas=obj }));
        }


    }
}