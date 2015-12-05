using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper
{
   public class keyS
    {

       /// <summary>
       /// 保存验证码的key
       /// </summary>
       public static string Vicode
       {
           get
           {
               return "Vicode";
           }
       }


       /// <summary>
       /// 用于保存队列中的消息key
       /// </summary>
       public static string Message
       {
           get
           {
               return "Message";
           }
       }
    }
}
