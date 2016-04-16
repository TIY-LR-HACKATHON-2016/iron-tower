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
                new FloorType() { BuildCost = 200, Category = FloorCategory.Business, Name = "Cafe", PeopleLimit = 2, Earning = 25, EarningIncrease = 2 },
                new FloorType() { BuildCost = 200, Category = FloorCategory.Apartment, Name = "Small Apartments", PeopleLimit = 2 },
                new FloorType() { BuildCost = 1500, Category = FloorCategory.Business, Name = "Pizzaria", PeopleLimit = 4, Earning = 50, EarningIncrease = 2 },
                new FloorType() { BuildCost = 2500, Category = FloorCategory.Apartment, Name = "Medium Apartments", PeopleLimit = 4 },
                new FloorType() { BuildCost = 5000, Category = FloorCategory.Business, Name = "Burger Joint", PeopleLimit = 5, Earning = 75, EarningIncrease = 2},
                new FloorType() { BuildCost = 7500, Category = FloorCategory.Apartment, Name = "Large Apartments", PeopleLimit = 5 }
                );
        }
    }
}
