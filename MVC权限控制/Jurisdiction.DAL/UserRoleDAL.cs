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
    using System.Data.SqlClient;
    public  class UserRoleDAL:DALBase<UserRole>,IDAL.UserRoleIDAL
    {



        //给指定用户分配角色
        public bool ApplayRoles(int uid, string roleids)
        {
            var rolieids = roleids.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            using (System.Transactions.TransactionScope scop = new System.Transactions.TransactionScope())
            {
                try
                {
                    //先删除原来的
                    var list = ds.Where(c => c.Uid == uid).ToList<Entity.UserRole>();
                    list.ForEach(c => Delete(c, true));
                    Save();
                    foreach (var item in rolieids)
                    {
                        Add(new UserRole() { Uid = uid, Roid = Convert.ToInt32(item) });
                    }
                    Save();
                    scop.Complete();
                    return true;
                }catch(Exception e){
                    throw e;
                }

            }
           
        }


        public bool BathDelete(string ids)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            string parametesrStr = string.Empty;
            SqlHelper.LoadSql(ids, ref parameters, ref parametesrStr);
            string sql = string.Format(" delete UserRole f where URid in({0})", parametesrStr);
            return je.Database.ExecuteSqlCommand(sql, parameters) > 0;
        }

        public bool BathDeleteByRoleId(string ids)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            string parametesrStr = string.Empty;
            SqlHelper.LoadSql(ids, ref parameters, ref parametesrStr);
            string sql = string.Format(" delete UserRole f where Roid in({0})", parametesrStr);
            return je.Database.ExecuteSqlCommand(sql, parameters) > 0;
        }
    }
}
