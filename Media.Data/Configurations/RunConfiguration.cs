using Media.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Data.Configurations
{
    public class RunConfiguration : EntityBaseConfiguration<Run>
    {
        public RunConfiguration()
        {
            Property(g => g.BatchId).IsRequired();
            Property(g => g.Sequence).IsRequired().HasMaxLength(100);
            Property(g => g.Direction).IsRequired();
            Property(g => g.TimeTaken).IsRequired();
        }
    }
}
