using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebHelper
{
    /// <summary>
    /// 处理json格式字符串相关信息
    /// </summary>
   public class JsonHelper
    {
       /// <summary>
       /// 将对象序列化为json字符串
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public static string SerializeObj(object obj)
       {
         return  new JavaScriptSerializer().Serialize(obj);
       }

       public static T DeSerialize<T>(string str)
       {
           return new JavaScriptSerializer().Deserialize<T>(str);
       }
    }
}
