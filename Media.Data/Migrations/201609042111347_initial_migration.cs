namespace Media.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batch",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatchNumbers = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Run",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        Sequence = c.String(nullable: false, maxLength: 100),
                        Direction = c.Int(nullable: false),
                        TimeTaken = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Batch", t => t.BatchId, cascadeDelete: true)
                .Index(t => t.BatchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Run", "BatchId", "dbo.Batch");
            DropIndex("dbo.Run", new[] { "BatchId" });
            DropTable("dbo.Run");
            DropTable("dbo.Batch");
        }
    }
}
