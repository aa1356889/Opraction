//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jurisdiction.IDAL
{
    using System;
    using System.Collections.Generic;using Jurisdiction.Entity;
    
    public  interface UsersIDAL:IBaseDAL<Users>
    {






        /// <summary>
        /// 根据id删除数据 多个用,号分割
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BathDelete(string ids);
    
    
    
    }
}
