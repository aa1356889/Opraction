using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web;
namespace WebHelper
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
   public class CaheManager
    {

       /// <summary>
       /// 将已绝对时间的方式存入缓存
       /// </summary>
       /// <param name="key"></param>
       /// <param name="obj"></param>
       public static void SetData(string key, object obj, int minute)
       {
           HttpContext.Current.Cache.Insert(key, obj, null, DateTime.Now.AddMinutes(minute), System.Web.Caching.Cache.NoSlidingExpiration);

       }

       /// <summary>
       /// 已相对时间将数据存入缓存
       /// </summary>
       /// <param name="key"></param>
       /// <param name="obj"></param>
       /// <param name="minute"></param>
       public static void SetDataInRelative(string key, object obj, int minute)
       {
           HttpContext.Current.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, minute, 0));
       }
       /// <summary>
       /// 从指定key中取得数据
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="key"></param>
       /// <returns></returns>
       public static T GetData<T>(string key)where T:class
       {
           object obj = HttpContext.Current.Cache.Get(key);
           if (obj == null)
           {
               return null;
           }
           return obj as T;
       }

       
    }
}
