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
            var users = userbll.QueryByPaging(requsetview.start, requsetview.length, c => c.IsDelete==0, c => c.uid, ref count).ToList<Users>().Select(c => new {c.uid,c.Uname,c.LoginName });
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
         [InitValidateUrl("ApplayRole")]
         public ActionResult GetRoles(int uid)
         {
             //获得用户拥有角色信息
             var urRole = userolebll.Query(c => c.Uid == uid).ToList<UserRole>().Select(c => new { c.Uid, c.Roid });
             //获得所有角色信息
            var roles = rolebll.Query(c => true).ToList<Roles>().Select(c =>new { c.Rid,c.RName,IsCheck=urRole.Any(r=>r.Roid==c.Rid)});
         
             return base.ProcessOkReData(roles);

         }


          [InitValidateUrl("ApplayRole")]
         public ActionResult AplayRole(int uid, string roleids)
         {
             if (userolebll.ApplayRoles(uid, roleids))
             {
                 return ProcessOk("分配成功");
             }
             else
             {
                 return ProcessNo("分配失败");
             }
         }


        /// <summary>
        /// 删除用户信息多个用，号隔开
        /// </summary>
        /// <returns></returns>
          public ActionResult DeleteUser(string ids)
          {
              if (userbll.BathDelete(ids))
              {
                  return ProcessOk("删除成功");
              }
              else
              {
                  return ProcessNo("删除失败");
              }
          }

    }
}
