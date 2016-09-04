using Media.Data.Configurations;
using Media.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Data
{
    public class MediaContext : DbContext
    {
        #region Entity Sets
        public IDbSet<Batch> BatchSet { get; set; }
        public IDbSet<Run> RunSet { get; set; }
        #endregion

        public MediaContext() : base("Media")
        {
            Database.SetInitializer<MediaContext>(null);
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BatchConfiguration());
            modelBuilder.Configurations.Add(new RunConfiguration());
        }
    }
}
