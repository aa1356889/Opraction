using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jurisdiction.DAL
{
    /// <summary>
    /// 公共的泛型数据层父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class DALBase<T>:IDAL.IBaseDAL<T>where T:class
    {

       jurisdictionEntities je = null;
        DbSet<T> ds = null;
        public DALBase(){
            je = new jurisdictionEntities();//初始化一个上下文对象
             ds=je.Set<T>();
        }

        public IQueryable<T> QueryByPaging<Tkey>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int count, params string[] tables)
        {
             DbQuery<T> dq=ds;//按需加载
            foreach (string table in tables)
            {
                dq = ds.Include(table);
            }
            return dq.Where(where).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize);
             
        }

        /// <summary>
        /// 发送一段sql脚本
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public List<T> QqueryBySql(string sqlText,params object[] paramters)
        {
           return ds.SqlQuery(sqlText,paramters).ToList<T>();
        }

        public void UpdateByProperty(T Model, params string[] PropertyNames)
        {
            //先将对象添加到实体代理类
            DbEntityEntry de=je.Entry(Model);
            foreach (var proName in PropertyNames)
            {
                de.Property(proName);
            }

        }

        public void Delete(T Model, bool isEfContext)
        {
            throw new NotImplementedException();
        }

        public void Add(T model)
        {
            throw new NotImplementedException();
        }


        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}