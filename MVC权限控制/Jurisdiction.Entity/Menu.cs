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
    
    public partial class Menu
    {
        public Menu()
        {
            this.Function = new HashSet<Function>();
            this.Opration = new HashSet<Opration>();
        }
    
        public int Mid { get; set; }
        public string MName { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ulr { get; set; }
    
        public virtual ICollection<Function> Function { get; set; }
        public virtual ICollection<Opration> Opration { get; set; }
    }
}
