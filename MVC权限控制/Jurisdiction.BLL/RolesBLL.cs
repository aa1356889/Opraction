//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jurisdiction.BLL
{
    using System;
    using System.Collections.Generic;using Jurisdiction.Entity;  using Jurisdiction.IBLL;  using Jurisdiction.IDAL;
    
    public  class RolesBLL:BaseBLL<Roles>,RolesIBLL
    {
        RolesIDAL idal = null;
        public RolesBLL(RolesIDAL dal):base(dal){
            this.idal = dal;
    
    
    
    
    
    }

        public bool BathDelete(string ids)
        {
           return  idal.BathDelete(ids);
        }
    }
}
