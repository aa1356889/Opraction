//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jurisdiction.DAL
{
    using System;
    using System.Collections.Generic;using Jurisdiction.Entity;
    using System.Linq;
    using Jurisdiction.EntityView;
    using Jurisdiction.Extend;
    public  class OprationDAL:DALBase< Jurisdiction.Entity.Opration>,IDAL.OprationIDAL
    {





        public List<OpractionsExtend> GetOpractionByUid(int uid)
        {
            var list = (from o in je.Opration
                        join m in je.Menu on o.Mid equals m.Mid
                        join f in je.Function on m.Mid equals f.Mid
                        join ur in je.UserRole on o.Roid equals ur.Roid
                        where ur.Uid == uid
                        select new OpractionsExtend() { Mares = m.Area, MContorll = m.Contorll, Action = f.Action, ActionName = f.Fname, MIco = m.Icon, Mid = m.Mid, MName = m.MName, Murl = m.url, PrentId = m.ParentId, Roid = o.Roid }).Distinct().ToList<OpractionsExtend>();
            return list;
        }


        /// <summary>
        /// 获得所有的权限数据
        /// </summary>
        /// <returns></returns>
        public List<OpractionsExtend> GetAllOperaction()
        {
            var list = (from o in je.Opration
                        join m in je.Menu on o.Mid equals m.Mid
                        join f in je.Function on m.Mid equals f.Mid
                        select new OpractionsExtend() { Mares = m.Area, MContorll = m.Contorll, Action = f.Action, ActionName = f.Fname, MIco = m.Icon, Mid = m.Mid, MName = m.MName, Murl = m.url, PrentId = m.ParentId, Roid = o.Roid }).Distinct().ToList<OpractionsExtend>();
            return list;
        }
    }
}
