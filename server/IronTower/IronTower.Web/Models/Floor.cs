using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;

namespace IronTower.Web.Models
{
    public class Floor
    {
     
        public int Id { get; set; }
        public bool isApartment { get; set; } = false; // set by type
        public int Earning { get; set; } // set by type
        public int PeopleLimit { get; set; } //set by type
        public int NumPeople { get; set; }
        public FloorType FloorType{ get; set; }

        //game balance
        public int EarningIncrease { get; set; } = 2; //per new employee
        public int BuildCost { get; set; } //set by type

        public virtual IEnumerable<Person> People { get; set; } = new List<Person>();

        public Floor(FloorType empty)
        {
            this.FloorType = empty;
        }

    }

    public enum FloorType
    {
        Empty = 1,
        SmallApartment = 2
    }

}