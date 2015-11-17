using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Jurisdiction.DAL
{
    /// <summary>
    /// 公共的泛型数据层父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class DALBase<T>:IDAL.IBaseDAL<T>where T:class
    {

       protected jurisdictionEntities je = null;
       protected  DbSet<T> ds = null;
        public DALBase(){
            //1.0 先从线程缓存CallContext中获取一个EF容器对象，如果没有则创建之
            object efContext = CallContext.GetData("BaseDbContext");
            if (efContext == null)
            {
                efContext = new jurisdictionEntities();

                //将新创建的EF容器对象存储到线程缓存中
                CallContext.SetData("BaseDbContext", efContext);
            }

            je= efContext as jurisdictionEntities;
             ds=je.Set<T>();
        }
      

        public IQueryable<T> QueryByPaging<Tkey>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int count, params string[] tables)
        {
             DbQuery<T> dq=ds;//按需加载
            foreach (string table in tables)
            {
                dq = ds.Include(table);
            
            }
            count = ds.Where(where).Count();
           
            return dq.Where(where).OrderBy(orderBy).Skip(pageIndex).Take(pageSize);
             
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
            de.State = System.Data.EntityState.Unchanged;
            foreach (var proName in PropertyNames)
            {
                de.Property(proName).IsModified = true;
            }
            je.Configuration.ValidateOnSaveEnabled = false;//关闭实体跟踪检查

        }

        /// <summary>
        /// 根据指定条件删除信息
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="isEfContext"></param>
        public void Delete(T Model, bool isEfContext)
        {
            if (Model == null)
            {
                throw new ArgumentNullException("model is null");
            }
            //如果不存在实体代理类
            if (!isEfContext)
            {
                ds.Attach(Model);
            }
            ds.Remove(Model);
             
        }

        public void Add(T model)
        {
            ds.Add(model);
        }


        public bool Save()
        {
           return je.SaveChanges()>0;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return ds.Where(where);
        }





        public bool BatchAdd(List<T> list)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var item in list)
                {
                    Add(item);
                }
                try{
                    Save();
               
                    scope.Complete();
                    return true;
               
                }catch(Exception e){
                 
                    scope.Dispose();
                    return false;
                }
            }
        }
    }
}