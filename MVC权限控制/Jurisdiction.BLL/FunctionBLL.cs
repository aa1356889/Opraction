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
    
    public  class FunctionBLL:BaseBLL<Function>,FunctionIBLL
    {

        FunctionIDAL dal = null;
        public FunctionBLL(FunctionIDAL dal):base(dal){

            this.dal = dal;
    
    
    
    
    }

        public bool BathDelete(string ids)
        {
          return  dal.BathDelete(ids);
        }
    }
}
