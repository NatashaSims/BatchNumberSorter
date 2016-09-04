using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Entities
{
    public class Batch : IEntityBase
    {
        public Batch()
        {
            Runs = new List<Run>();
        }
        public int ID { get; set; }
        public string BatchNumbers { get; set; }
        public virtual ICollection<Run> Runs { get; set; }
    }
}
