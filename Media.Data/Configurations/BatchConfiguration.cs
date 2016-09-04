using Media.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Data.Configurations
{
    public class BatchConfiguration : EntityBaseConfiguration<Batch>
    {
        public BatchConfiguration()
        {
            Property(g => g.BatchNumbers).IsRequired().HasMaxLength(100);
        }
    }
}
