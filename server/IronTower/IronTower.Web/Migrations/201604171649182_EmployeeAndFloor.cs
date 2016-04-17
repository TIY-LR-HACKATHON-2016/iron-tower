namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeAndFloor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "MoneyPerMin");
            DropColumn("dbo.Games", "NextFloorCost");
            DropColumn("dbo.Games", "Message");
            DropColumn("dbo.Games", "MessageType");
            DropColumn("dbo.Games", "Unemployed");
            DropColumn("dbo.Games", "NextFloorCostIncrease");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "NextFloorCostIncrease", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "Unemployed", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "MessageType", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "Message", c => c.String());
            AddColumn("dbo.Games", "NextFloorCost", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "MoneyPerMin", c => c.Int(nullable: false));
        }
    }
}
