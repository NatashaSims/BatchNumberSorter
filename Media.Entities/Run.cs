using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Entities
{
    public class Run : IEntityBase
    {
        public int ID { get; set; }
        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        public string Sequence { get; set; }
        public Direction Direction { get; set; }
        public Decimal TimeTaken { get; set; }
    }
}
