using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper
{
   public class ResourceUrlHelper
    {
        //js css 的资源文件夹
        static string ResourceKey = "resorece";

        static string ResourceNumKkey = "ResourceNumKkey";//资源版本号key
        /// <summary>
        /// 加载资源路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadResource(string path)
        {
            string url = ConfigHelper.LoadConfigAppction(ResourceKey) + "/" + path + "?v=" + ConfigHelper.LoadConfigAppction(ResourceNumKkey);
            return ConfigHelper.LoadConfigAppction(ResourceKey) + "/" + path;
        }

        public static string LoadAreasResouce(string path)
        {
            return "/Areas" + path;
        }
    }
}
