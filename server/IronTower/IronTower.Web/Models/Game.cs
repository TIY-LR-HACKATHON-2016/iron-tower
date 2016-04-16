using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronTower.Web.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int MoneyPerMin { get; set; }
        public int NextFloorCost { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public int PeopleLimit { get; set; }
        public int Unemployed { get; set; }
        public DateTime LastTenant { get; set; } = new DateTime();

        public virtual IEnumerable<Person> People { get; set; } = new List<Person>();
        public virtual IEnumerable<Floor> Tower { get; set; } = new List<Floor>();
    }
}