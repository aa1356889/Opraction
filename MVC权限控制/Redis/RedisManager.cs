using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using WebHelper;
namespace Redis
{
   public class RedisManager
    {
        /// <summary>
        ///链接redis服务
        /// </summary>
        static RedisClient redis = new RedisClient(ConfigHelper.RedisUrl,Convert.ToInt32(ConfigHelper.RedisPort));

        #region 模拟队列
        /// <summary>
        /// 将数据从列表前插入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Lpush(string key, object value)
        {
            redis.LPush(key, ByteConvertHelper.Object2Bytes(value));
        }

        /// <summary>
        ///从列表前出去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T LPop<T>(string key)
            where T : class
        {
            byte[] bytes = redis.LPop(key);
            return ByteConvertHelper.Bytes2Object(bytes) as T;
        } 
        #endregion
    }
}
