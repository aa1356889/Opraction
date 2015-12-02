using Jurisdiction.EntityView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jurisdiction.Extend
{
    /// <summary>
    /// 权限扩展类
    /// </summary>
   public class OpractionsExtend:Opration
    {
       /// <summary>
       /// 角色名字
       /// </summary>
       public string RName { get; set; }

       /// <summary>
       /// 浏览菜单名字
       /// </summary>
       public string MName { get; set; }

       /// <summary>
       /// 菜单定位url
       /// </summary>
       public string Murl { get; set; }


       /// <summary>
       ///菜单父id
       /// </summary>
       public int PrentId { get; set; }



       /// <summary>
       /// 菜单图标
       /// </summary>
       public string MIco { get; set; }

       /// <summary>
       /// 菜单所属区域
       /// </summary>
       public string Mares { get; set; }

       /// <summary>
       /// f菜单所属控制器
       /// </summary>
       public string MContorll { get; set; }

       /// <summary>
       /// 所属控制器
       /// </summary>
       public string Action { get; set; }

       /// <summary>
       /// 菜单所属功能按钮名字
       /// </summary>
       public string ActionName { get; set; }


    }
}
