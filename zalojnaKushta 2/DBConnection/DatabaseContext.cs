using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalojnaKushta_2.Entity;

namespace zalojnaKushta_2.DBConnection
{
    class DatabaseContext: DbContext
    {
        public DatabaseContext() : base(Properties.Settings.Default.DBConnection) { }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<DealEntity> Deals { get; set; }
        public DbSet<PladgedEntity> Pladgeds { get; set; }

        public DbSet<LogEntity> Logs { get; set; }

    }
}
