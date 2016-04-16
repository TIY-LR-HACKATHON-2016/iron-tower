﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronTower.Web.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; } = 600; //starting money should match starting nextfloorcost
        public int MoneyPerMin { get; set; }
        public int NextFloorCost { get; set; } = 600; //starting
        public string Message { get; set; }
        public int MessageType { get; set; } //0 nothing, 1 good, 2 bad
        public int PeopleLimit { get; set; }
        public int Unemployed { get; set; }
        public DateTime LastTenant { get; set; } = new DateTime();
        public DateTime LastPaid { get; set; } = new DateTime();

        //game balance
        public int TennantInterval { get; set; } = 60; //seconds
        public int NextFloorCostIncrease { get; set; } = 2; //multiplier

        public virtual IEnumerable<Person> People { get; set; } = new List<Person>();
        public virtual IEnumerable<Floor> Tower { get; set; } = new List<Floor>();
    }
}