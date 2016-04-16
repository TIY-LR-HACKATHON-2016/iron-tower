namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialTables : DbMigration
    {
        public override void Up()
        {
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
                        PeopleLimit = c.Int(nullable: false),
                        Unemployed = c.Int(nullable: false),
                        LastTenant = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Games");
        }
    }
}
