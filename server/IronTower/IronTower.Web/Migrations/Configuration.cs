namespace IronTower.Web.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IronTowerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IronTowerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.FloorTypes.AddOrUpdate(x => x.Name,
                new FloorType() { BuildCost = 0, Category = FloorCategory.Empty, Name = "Empty Floor", },
                new FloorType() { BuildCost = 200, Category = FloorCategory.Business, Name = "McDonalds", PeopleLimit = 2, Earning = 25, EarningIncrease = 2 },
                new FloorType() { BuildCost = 200, Category = FloorCategory.Apartment, Name = "Bungelo", PeopleLimit = 2 }
                );
        }
    }
}
