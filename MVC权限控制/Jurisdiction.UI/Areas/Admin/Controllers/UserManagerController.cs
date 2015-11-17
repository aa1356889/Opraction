using Jurisdiction.Entity;
using Jurisdiction.UI.Areas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper.Attrbute;

namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    public class UserManagerController : MyControllers
    {

        IBLL.UsersIBLL userbll = null;
        IBLL.RolesIBLL rolebll = null;
        IBLL.UserRoleIBLL userolebll = null;
        public UserManagerController(IBLL.UsersIBLL iuserbll,IBLL.RolesIBLL irolebll,IBLL.UserRoleIBLL urbll)
        {
            userbll = iuserbll;
            rolebll = irolebll;
            userolebll = urbll;
        }
        // GET: /Admin/User/

        public ActionResult Index()
        {
            return View();
        }

         [InitValidateUrl("Index")]
        public ActionResult GetUsers(RquestPagingData requsetview)
        {
            ResponsPagingData responseData = new ResponsPagingData();
            int count = 0;
            //查询所有角色信息
            var users = userbll.QueryByPaging(requsetview.star, requsetview.length, c => true, c => c.uid, ref count).ToList<Users>().Select(c => new {c.uid,c.Uname,c.LoginName });
            responseData.data = users;
            responseData.draw = requsetview.draw;
            responseData.recordsFiltered = count;
            responseData.recordsTotal = count;
            return Json(responseData);
        }

         public ActionResult ApplayRole()
         {
             return View();
         }


        /// <summary>
        /// 获得所有角色信息和用户拥有角色信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
         public ActionResult GetRoles(int uid)
         {
             //获得所有角色信息
            var roles = rolebll.Query(c => true).ToList<Roles>().Select(c =>new { c.Rid,c.RName});
             //获得用户拥有角色信息
           var urRole = userolebll.Query(c => c.Uid == uid).ToList<UserRole>().Select(c => new { c.Uid,c.Roid});
           return base.ProcessOkReData(new { Roles = roles, UrRole = urRole });

         }

    }
}
