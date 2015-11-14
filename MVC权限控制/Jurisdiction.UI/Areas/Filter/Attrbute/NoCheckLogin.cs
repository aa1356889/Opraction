using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jurisdiction.UI.Areas.Filter.Attrbute
{
    /// <summary>
    /// 用于标识 如果打上了此过滤器则不进行登陆验
    /// </summary>
    public class NoCheckLoginAttribute:Attribute
    {
    }
}