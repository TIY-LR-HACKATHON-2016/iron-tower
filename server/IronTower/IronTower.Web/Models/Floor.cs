using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;

namespace IronTower.Web.Models
{
    public class Floor
    {
     
        public int Id { get; set; }
        public int Earning { get; set; } = 0;
        public int Interval { get; set; } = 0;
        public DateTime LastEarned { get; set; } = new DateTime();
        public int PeopleLimit { get; set; } = 0;
        public int NumPeople { get; set; } = 0;
        public FloorType FloorType{ get; set; }

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