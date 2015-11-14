using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jurisdiction.UI.Areas.Model
{
    /// <summary>
    /// 用于接收请求的参数
    /// </summary>
    public class RquestPagingData
    {
        /// <summary>
        /// 用户请求数据正常往返的数据
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 开始下标
        /// </summary>
        public int star { get; set; }


        /// <summary>
        /// 结束下标
        /// </summary>
        public int length { get;set; }
    }
}