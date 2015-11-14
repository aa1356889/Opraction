using Jurisdiction.Entity;
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

        public DbSet<Function> Function { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Opration> Opration { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
