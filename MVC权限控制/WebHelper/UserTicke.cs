using Jurisdiction.Entity;
using Jurisdiction.Entity.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebHelper
{
    // <summary>
    /// 用于保存当前用户的一些基础信息
    /// </summary>
    public class UserTick : IPrincipal
    {


        /// <summary>
        /// 用户id
        /// </summary>
        public int UId { get; set; }

        /// <summary>
        /// 用户登陆名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string Uanme { get; set; }



        [ScriptIgnore]
        public IIdentity Identity
        {
            get { return Identity; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
