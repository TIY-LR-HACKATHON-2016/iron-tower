namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloorFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "LastPaid", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "TennantInterval", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "NextFloorCostIncrease", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "TotalFloorTypes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "TotalFloorTypes");
            DropColumn("dbo.Games", "NextFloorCostIncrease");
            DropColumn("dbo.Games", "TennantInterval");
            DropColumn("dbo.Games", "LastPaid");
        }
    }
}
