using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
   public class ConfigHelper
    {
       /// <summary>
        /// 读取配置文件AppSettings节点
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       public static  string LoadConfigAppction(string key)
       {
           return System.Configuration.ConfigurationManager.AppSettings[key];
       }

     
       #region reids 相关配置信息
       public static string RedisUrl
       {
           get
           {
               return LoadConfigAppction("redisUrl");
           }
       }

       public static string RedisPort
       {
           get
           {
               return LoadConfigAppction("redisPort");
           }
       } 
       #endregion
    }
}
