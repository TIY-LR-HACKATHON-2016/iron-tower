namespace IronTower.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedSchemaError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Home_Id", "dbo.Floors");
            DropIndex("dbo.People", new[] { "Home_Id" });
            RenameColumn(table: "dbo.People", name: "Home_Id", newName: "HomeId");
            RenameColumn(table: "dbo.People", name: "Work_Id", newName: "WorkId");
            RenameIndex(table: "dbo.People", name: "IX_Work_Id", newName: "IX_WorkId");
            AlterColumn("dbo.People", "HomeId", c => c.Int(nullable: false));
            CreateIndex("dbo.People", "HomeId");
            AddForeignKey("dbo.People", "HomeId", "dbo.Floors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "HomeId", "dbo.Floors");
            DropIndex("dbo.People", new[] { "HomeId" });
            AlterColumn("dbo.People", "HomeId", c => c.Int());
            RenameIndex(table: "dbo.People", name: "IX_WorkId", newName: "IX_Work_Id");
            RenameColumn(table: "dbo.People", name: "WorkId", newName: "Work_Id");
            RenameColumn(table: "dbo.People", name: "HomeId", newName: "Home_Id");
            CreateIndex("dbo.People", "Home_Id");
            AddForeignKey("dbo.People", "Home_Id", "dbo.Floors", "Id");
        }
    }
}
