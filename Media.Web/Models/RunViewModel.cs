using Media.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Media.Web.Models
{
    public class RunViewModel
    {
        public int ID { get; set; }
        public int BatchId { get; set; }
        public string Batch { get; set; }
        public string Sequence { get; set; }
        public Direction Direction { get; set; }
        public Decimal TimeTaken { get; set; }
    }
}