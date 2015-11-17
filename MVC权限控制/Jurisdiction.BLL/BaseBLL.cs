using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jurisdiction.BLL
{
   public class BaseBLL<T>:IBLL.IBaseBLL<T> where T:class
    {
      protected IDAL.IBaseDAL<T> baseDal = null;
       public BaseBLL(IDAL.IBaseDAL<T> basedal)
       {
           baseDal = basedal;
       }
        public IQueryable<T> QueryByPaging<Tkey>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int count, params string[] tables)
        {
          return  baseDal.QueryByPaging<Tkey>(pageIndex, pageSize, where, orderBy, ref count, tables);
             
        }

        /// <summary>
        /// 发送一段sql脚本
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public List<T> QqueryBySql(string sqlText,params object[] paramters)
        {
            return baseDal.QqueryBySql(sqlText, paramters);
        }

        public void UpdateByProperty(T Model, params string[] PropertyNames)
        {
            baseDal.UpdateByProperty(Model);

        }

        public void Delete(T Model, bool isEfContext)
        {
            baseDal.Delete(Model, isEfContext);
        }

        public void Add(T model)
        {
            baseDal.Add(model);
        }


        public bool Save()
        {
          return  baseDal.Save();
        }


        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return baseDal.Query(where);
        }


        public bool BatchAdd(List<T> list)
        {
            return baseDal.BatchAdd(list);
        }
    }
}
