using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IronTower.Data2;

public class IronTowerDBContext : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public IronTowerDBContext() : base("name=IronTowerDBContext")
    {
    }


    public System.Data.Entity.DbSet<Game> Games { get; set; }
    public System.Data.Entity.DbSet<Person> Persons { get; set; }
    public System.Data.Entity.DbSet<Floor> Floors { get; set; }
    public System.Data.Entity.DbSet<FloorType> FloorTypes { get; set; }

}
