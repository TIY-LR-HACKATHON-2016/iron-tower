namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FloorType_Id = c.Int(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloorTypes", t => t.FloorType_Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.FloorType_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.FloorTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.Int(nullable: false),
                        Earning = c.Int(nullable: false),
                        PeopleLimit = c.Int(nullable: false),
                        EarningIncrease = c.Int(nullable: false),
                        BuildCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeId = c.Int(nullable: false),
                        WorkId = c.Int(),
                        Name = c.String(),
                        Game_Id = c.Int(),
                        Floor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Floors", t => t.HomeId, cascadeDelete: true)
                .ForeignKey("dbo.Floors", t => t.WorkId)
                .ForeignKey("dbo.Floors", t => t.Floor_Id)
                .Index(t => t.HomeId)
                .Index(t => t.WorkId)
                .Index(t => t.Game_Id)
                .Index(t => t.Floor_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Money = c.Int(nullable: false),
                        MoneyPerMin = c.Int(nullable: false),
                        NextFloorCost = c.Int(nullable: false),
                        Message = c.String(),
                        MessageType = c.Int(nullable: false),
                        Unemployed = c.Int(nullable: false),
                        LastTenant = c.DateTime(nullable: false),
                        LastPaid = c.DateTime(nullable: false),
                        TennantInterval = c.Int(nullable: false),
                        NextFloorCostIncrease = c.Int(nullable: false),
                        TotalFloorTypes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Floor_Id", "dbo.Floors");
            DropForeignKey("dbo.People", "WorkId", "dbo.Floors");
            DropForeignKey("dbo.People", "HomeId", "dbo.Floors");
            DropForeignKey("dbo.Floors", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.People", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Floors", "FloorType_Id", "dbo.FloorTypes");
            DropIndex("dbo.People", new[] { "Floor_Id" });
            DropIndex("dbo.People", new[] { "Game_Id" });
            DropIndex("dbo.People", new[] { "WorkId" });
            DropIndex("dbo.People", new[] { "HomeId" });
            DropIndex("dbo.Floors", new[] { "Game_Id" });
            DropIndex("dbo.Floors", new[] { "FloorType_Id" });
            DropTable("dbo.Games");
            DropTable("dbo.People");
            DropTable("dbo.FloorTypes");
            DropTable("dbo.Floors");
        }
    }
}
