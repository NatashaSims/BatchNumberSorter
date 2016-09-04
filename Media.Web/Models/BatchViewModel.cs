using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Media.Web.Models
{
    public class BatchViewModel
    {
        public int ID { get; set; }
        public string BatchNumbers { get; set; }
        public int NumberOfRuns { get; set; }
    }
}