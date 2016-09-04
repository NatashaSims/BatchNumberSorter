using Media.Entities;
using Media.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Media.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateBatch(this Batch batch, BatchViewModel batchVm)
        {
            batch.BatchNumbers = batchVm.BatchNumbers;
        }

        public static void UpdateRun(this Run run, RunViewModel runVm)
        {
            run.BatchId = runVm.BatchId;
            run.Direction = runVm.Direction;
            run.Sequence = runVm.Sequence;
            run.TimeTaken = runVm.TimeTaken;
        }

    }
}