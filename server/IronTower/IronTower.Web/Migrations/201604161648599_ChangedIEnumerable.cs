namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedIEnumerable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Floor_Id = c.Int(),
                        Home_Id = c.Int(),
                        Work_Id = c.Int(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Floors", t => t.Floor_Id)
                .ForeignKey("dbo.Floors", t => t.Home_Id)
                .ForeignKey("dbo.Floors", t => t.Work_Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Floor_Id)
                .Index(t => t.Home_Id)
                .Index(t => t.Work_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        isApartment = c.Boolean(nullable: false),
                        Earning = c.Int(nullable: false),
                        PeopleLimit = c.Int(nullable: false),
                        NumPeople = c.Int(nullable: false),
                        FloorType = c.Int(nullable: false),
                        EarningIncrease = c.Int(nullable: false),
                        BuildCost = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Floors", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.People", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.People", "Work_Id", "dbo.Floors");
            DropForeignKey("dbo.People", "Home_Id", "dbo.Floors");
            DropForeignKey("dbo.People", "Floor_Id", "dbo.Floors");
            DropIndex("dbo.Floors", new[] { "Game_Id" });
            DropIndex("dbo.People", new[] { "Game_Id" });
            DropIndex("dbo.People", new[] { "Work_Id" });
            DropIndex("dbo.People", new[] { "Home_Id" });
            DropIndex("dbo.People", new[] { "Floor_Id" });
            DropTable("dbo.Floors");
            DropTable("dbo.People");
        }
    }
}
