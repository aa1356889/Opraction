using Jurisdiction.Entity;
using Jurisdiction.UI.Areas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper;
using System.Linq;
namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    public class RoleManagerController :MyControllers
    {
        //
        // GET: /Admin/RoleManage/
        IBLL.RolesIBLL rolebll = null;
        public RoleManagerController(IBLL.RolesIBLL bll)
        {
            rolebll = bll;
        }
        public ActionResult Index()
        {
           
            return View();
        }

        /// <summary>
        /// 获得所有角色列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRols(RquestPagingData requsetview)
        {
            ResponsPagingData responseData = new ResponsPagingData();
            int count=0;
           //查询所有角色信息
            var roles = rolebll.QueryByPaging(requsetview.star, requsetview.length, c => true, c => c.Rid, ref count).ToList<Roles>().Select(c =>new{Rid=c.Rid,RName=c.RName });
           responseData.data = roles;
           responseData.draw = requsetview.draw;
           responseData.recordsFiltered = count;
           responseData.recordsTotal = count;
           return Json(responseData);
        }

    }
}
