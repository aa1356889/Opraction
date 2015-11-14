using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jurisdiction.UI.Areas.Model
{
    /// <summary>
    /// datatables响应给客户端的数据
    /// </summary>
    public class ResponsPagingData
    {
        /// <summary>
        /// 记录本次请求的数据起始位置
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 服务器过滤后的总数据量
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object data { get; set; }
    }
}