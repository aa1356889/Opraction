using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingalR
{
    /// <summary>
    /// 存入队列的数据信息
    /// </summary>
   public class Message
    {
       /// <summary>
       /// 发送人
       /// </summary>
        public string SendUser { get; set; }

       /// <summary>
       /// 接收人
       /// </summary>
        public string RevidUser { get; set; }

       /// <summary>
       ///消息
       /// </summary>
        public string Content { get; set; }


       /// <summary>
       /// 发送时间
       /// </summary>
        public DateTime SendTime { get; set; }
    }
}
