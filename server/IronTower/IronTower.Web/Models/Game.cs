using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IronTower.Web.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Player"; //change in game creation
        public int Money { get; set; } = 50000; //starting money watch first 

        public DateTime LastTenant { get; set; } = DateTime.Now;
        public DateTime LastPaid { get; set; } = DateTime.Now;

        [NotMapped]
        public int NextFloorCost
        {
            get
            {
                var numOfFloors = Tower.Count();
                return (int) (FirstFloorCost*Math.Pow(1.5, numOfFloors - 1));
            }
        }

        //game balance
        public int TennantInterval { get; set; } = 2; //seconds
        public int FirstFloorCost => 600; //starting

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
        public virtual ICollection<Floor> Tower { get; set; } = new List<Floor>();
    }
}