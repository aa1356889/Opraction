using Jurisdiction.Entity;
using Jurisdiction.UI.Areas.Admin.Models.Menu;
using Jurisdiction.UI.Areas.Filter.Attrbute;
using Jurisdiction.UI.Areas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper.Attrbute;

namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    public class MenuController :MyControllers
    {
        IBLL.MenuIBLL imenubll = null;
        IBLL.FunctionIBLL ifunctionbll = null;
        public MenuController(IBLL.MenuIBLL menubll,IBLL.FunctionIBLL fbll)
        {
            imenubll = menubll;
            ifunctionbll = fbll;
        }

        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获得所有菜单信息
        /// </summary>
        /// <returns></returns>
        [InitValidateUrl("Index")]
        public ActionResult GetMenus()
        {
            var menus = imenubll.Query(c => true).ToList<Entity.Menu>().Select(c => new { c.Area,c.Contorll,c.Mid,c.Icon,c.MName,c.ParentId,c.url});
         
            return ProcessOkReData(menus);
        }

        /// <summary>
        /// 获得指定菜单的所有功能按钮
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
       [InitValidateUrl("Index")]
        public ActionResult GetFunctions(MenuRequestView requsetview)
        {
            int count = 0;
          var functions= ifunctionbll.QueryByPaging(requsetview.start, requsetview.length, c =>c.Mid==requsetview.mid,c=>c.Fid,ref count).ToList<Function>().Select(c => new { c.Fid, c.Fname, c.Action });
          ResponsPagingData responseData = new ResponsPagingData();
          responseData.data = functions;
          responseData.draw = requsetview.draw;
          responseData.recordsFiltered = count;
          responseData.recordsTotal = count;
          return Json(responseData);
        }

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOpraction(Entity.Function fun)
        {
            if (string.IsNullOrEmpty(fun.Action) || fun.Mid == 0 || string.IsNullOrEmpty(fun.Fname))
            {
                return ProcessNo("参数不合法");
            }
            ifunctionbll.Add(fun);
            if (ifunctionbll.Save())
            {
                return ProcessOk("添加权限成功");
            }else{
                return ProcessNo("添加权限失败");
            }
        }


        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public ActionResult EditOpraction(Entity.Function fun)
        {
            if (fun.Fid==0)
            {
                return ProcessNo("参数不合法");
            }
            Function oldFunction=ifunctionbll.Query(c => c.Fid == fun.Fid).ToList<Function>().FirstOrDefault();
            oldFunction.Fname = fun.Fname;
            oldFunction.Action = fun.Action;
            if (ifunctionbll.Save())
            {
                return ProcessOk("修改成功");
            }
            else
            {
                return ProcessNo("修改失败");
            }
        }

    }
}
