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
    using System.Data.SqlClient;
    
    public  class UsersDAL:DALBase<Users>,IDAL.UsersIDAL
    {









        public bool BathDelete(string ids)
        {

                //删除用户信息
                List<SqlParameter> parameters = new List<SqlParameter>();
                string parametesrStr = string.Empty;
                SqlHelper.LoadSql(ids, ref parameters, ref parametesrStr);
                string sql = string.Format("update Users set isdelete=1  where uid in({0})", parametesrStr);
                return je.Database.ExecuteSqlCommand(sql,parameters.ToArray()) > 0;
        }
    }
}
