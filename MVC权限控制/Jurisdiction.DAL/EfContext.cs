using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jurisdiction.DAL
{
    /// <summary>
    /// EF上下文对象
    /// </summary>
    public partial class jurisdictionEntities : DbContext
    {
        public jurisdictionEntities()
            : base("name=jurisdictionEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            jurisdictionEntities je = new jurisdictionEntities();
            throw new UnintentionalCodeFirstException();
        }

    }
}
