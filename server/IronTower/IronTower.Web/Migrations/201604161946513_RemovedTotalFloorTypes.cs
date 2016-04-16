namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTotalFloorTypes : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "TotalFloorTypes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "TotalFloorTypes", c => c.Int(nullable: false));
        }
    }
}
