namespace Media.Data.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Media.Data.MediaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Media.Data.MediaContext context)
        {
            context.BatchSet.AddOrUpdate(g => g.BatchNumbers, GenerateBatches());
            context.RunSet.AddOrUpdate(m => m.Sequence, GenerateRuns());
        }

        private Batch[] GenerateBatches()
        {
            Batch[] batches = new Batch[] {
                new Batch() { BatchNumbers = "3,14,15,2,1" },
                new Batch() { BatchNumbers = "32,14,135,2,1,4" },
                new Batch() { BatchNumbers = "3,142,15,22,12" },
            };

            return batches;
        }
        private Run[] GenerateRuns()
        {
            Run[] runs = new Run[] {
                new Run()
                {   
                    BatchId = 1,
                    Sequence = "1,2,3,14,15",
                    Direction = Direction.ascending,
                    TimeTaken = 9.00m

                },
                new Run()
                {
                    BatchId = 1,
                    Sequence = "15,14,3,2,1",
                    Direction = Direction.descending,
                    TimeTaken = 3.00m
                },
                new Run()
                {
                    BatchId = 2,
                    Sequence = "1,2,4,14,32,135",
                    Direction = Direction.ascending,
                    TimeTaken = 12.00m
                },
                new Run()
                {
                    BatchId = 3,
                    Sequence = "3,12,15,22,142",
                    Direction = Direction.ascending,
                    TimeTaken = 4.00m
                },
                new Run()
                {
                    BatchId = 3,
                    Sequence = "142,22,15,12,3",
                    Direction = Direction.descending,
                    TimeTaken = 10.00m
                },             
            };

            return runs;
        }
     
    }
}
