using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronTower.Web.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Player"; //change in game creation
        public int Money { get; set; } = 1000; //starting money watch first 
        public int MoneyPerMin { get; set; } = 10; // starting
        public int NextFloorCost { get; set; } = 600; //starting
        public string Message { get; set; }
        public int MessageType { get; set; } //0 nothing, 1 good, 2 bad
        public int PeopleLimit { get; set; }
        public int Unemployed { get; set; }
        public DateTime LastTenant { get; set; } = DateTime.Now;
        public DateTime LastPaid { get; set; } = DateTime.Now;

        //game balance
        public int TennantInterval { get; set; } = 60; //seconds
        public int NextFloorCostIncrease { get; set; } = 2; //multiplier
        public int TotalFloorTypes { get; set; } = 2; //change as add floors (ignore 0)

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
        public virtual ICollection<Floor> Tower { get; set; } = new List<Floor>();

        public Game()
        {

        }

        public Game(string name)
        {
            Name = name;
        }
    }
}