using Jurisdiction.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jurisdiction.IDAL
{
   public interface IBaseDAL<T> where T:class
    {

       /// <summary>
       /// 根据指定条件查询用户数据
       /// </summary>
       /// <param name="where"></param>
       /// <returns></returns>
       IQueryable<T> Query(Expression<Func<T, bool>> where);

       /// <summary>
       /// 分页查询数据
       /// </summary>
       /// <param name="pageIndex">页码</param>
       /// <param name="pageSize">页容量</param>
       /// <param name="where">查询条件</param>
       /// <param name="count">返回的总页数</param>
       /// <returns></returns>
       IQueryable<T> QueryByPaging<Tkey>(int pageIndex, int pageSize, Expression<Func<T, bool>> where,Expression<Func<T, Tkey>> orderBy, ref int count, params string[] tables);

       /// <summary>
       /// 按需修改实体对象 必须有主键信息
       /// </summary>
       /// <param name="Model">修改的实体对象</param>
       /// <param name="PropertyNames">修改的属性</param>
       /// <returns></returns>
       void UpdateByProperty(T Model, params string[] PropertyNames);

       
       /// <summary>
       /// 删除数据
       /// </summary>
       /// <param name="Model">删除的实体对象</param>
       /// <param name="isEfContext">是否存在ef上下文</param>
       /// <returns></returns>
       void Delete(T Model,bool isEfContext);


       /// <summary>
       /// 添加指定实现对象信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       void Add(T model);


       List<T> QqueryBySql(string sqlText,params object[] parameters);

       /// <summary>
       ///生成sql语句发往服务器
       /// </summary>
       /// <returns></returns>
       bool Save();
      
    }
}
