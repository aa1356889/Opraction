using Jurisdiction.Entity;
using Jurisdiction.Extend;
using Jurisdiction.UI.Areas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHelper;
using WebHelper.Attrbute;
namespace Jurisdiction.UI.Areas.Admin.Controllers
{
    public class RoleManagerController :MyControllers
    {
        //
        // GET: /Admin/RoleManage/
        IBLL.RolesIBLL rolebll = null;
        IBLL.MenuIBLL iMbll = null;
        IBLL.FunctionIBLL fBll = null;
        public RoleManagerController(IBLL.RolesIBLL bll,IBLL.MenuIBLL mbll,IBLL.FunctionIBLL fuctionBll)
        {
            rolebll = bll;
            iMbll = mbll;
            fBll = fuctionBll;
        }
        public ActionResult Index()
        {
           
            return View();
        }

        /// <summary>
        /// 获得所有角色列表
        /// </summary>
        /// <returns></returns>
       [InitValidateUrl("Index")]
        public ActionResult GetRols(RquestPagingData requsetview)
        {
            ResponsPagingData responseData = new ResponsPagingData();
            int count=0;
           //查询所有角色信息
            var roles = rolebll.QueryByPaging(requsetview.start, requsetview.length, c => true, c => c.Rid, ref count).ToList<Roles>().Select(c =>new{Rid=c.Rid,RName=c.RName });
           responseData.data = roles;
           responseData.draw = requsetview.draw;
           responseData.recordsFiltered = count;
           responseData.recordsTotal = count;
           return Json(responseData);
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
       public ActionResult AddRole(string roleName)
       {
           Roles role = new Roles() { RName = roleName };
           rolebll.Add(role);
           if (rolebll.Save())
           {
               return base.ProcessOk("新增角色成功");
           }
           else
           {
               return base.ProcessNo("新增角色失败");
           }
       }

        /// <summary>
        /// 获得所有权限数据
        /// </summary>
        /// <returns></returns>
        [InitValidateUrl("SetOpreaction")]
        public ActionResult GetAllOpraction(string rid)
        {
            int intRid=0;
            if (string.IsNullOrEmpty(rid)||!int.TryParse(rid,out intRid))
            {
                return base.ProcessNo("参数不合法");
            }
            //获得当前角色所有用权限
            List<OpractionsExtend> opraction = Opracitonbll.GetOpractionByRoid(intRid);
            //获得所有菜单数据
          var  menus=iMbll.Query(c => true).ToList<Menu>().Select(c=>new{c.Mid,c.MName,c.ParentId});
            //获得所有菜单里面的功能数据
          var functions = fBll.Query(c => true).ToList<Function>().Select(c=>new {c.Fid,c.Mid,c.Fname,isCheck=opraction.Any(o=>o.Fid==c.Fid)});
       
          return base.ProcessOkReData(new { Menus = menus, Functions = functions });
        }
        /// <summary>
        /// 获得设置权限数据的视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SetOpreaction()
        {
            return View();
        }


        /// <summary>
        /// 设置权限
        /// </summary>
        /// <returns></returns>
          [InitValidateUrl("SetOpreaction")]
        public ActionResult Settion(string rid, string opractions)
        {
            int intRid = 0;
            if (string.IsNullOrEmpty(opractions) || !int.TryParse(rid, out intRid))
            {
                return base.ProcessNo("参数不合法");
            }
            List<Opration> opraction = new List<Opration>();
            string[] opractionArr=opractions.Split(new char[] { ',' });
            string[] marr = null;
            foreach (var item in opractionArr)
            {
                marr=item.Split(new char[] { '|' });
                if (marr == null || marr.Length < 2)
                {
                    continue;
                }
                opraction.Add(new Opration() { Fid = Convert.ToInt32(marr[1]), Mid = Convert.ToInt32(marr[0]), Roid = intRid });
            }
            if (Opracitonbll.SetOpractionByRid(intRid, opraction))
            {
                return base.ProcessOk("设置成功");
            }
            else
            {
                return base.ProcessNo("设置失败");
            }

        }

    }
}
