//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jurisdiction.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public Users()
        {
            this.UserRole = new HashSet<UserRole>();
        }
    
        public int uid { get; set; }
        public string UpassWord { get; set; }
        public string Uname { get; set; }
        public string LoginName { get; set; }
        public string HeadPortraitUrl { get; set; }
        public int IsDelete { get; set; }
    
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
