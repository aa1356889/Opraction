using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingalR
{
    /// <summary>
    /// 用于保存 在线用户信息
    /// </summary>
   public class User
    {
       /// <summary>
       /// 用户的链接标示
       /// </summary>
        public string ConnctionId { get; set; }

       /// <summary>
       /// 用户id
       /// </summary>
        public string Uid { get; set; }

       /// <summary>
       /// 用户登陆名
       /// </summary>
        public string LoginName { get; set; }
    }
}
