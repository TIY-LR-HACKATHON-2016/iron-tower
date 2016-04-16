using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;

namespace IronTower.Web.Models
{
    public class Floor
    {
     
        public int Id { get; set; }
        public int Earning { get; set; }
        public int Interval { get; set; }
        public DateTime LastEarned { get; set; } = new DateTime();
        public int PeopleLimit { get; set; }
        public int NumPeople{ get; set; }
        public FloorType FloorType{ get; set; }

        public virtual IEnumerable<Person> People { get; set; } = new List<Person>();

        public Floor(FloorType empty)
        {
            this.FloorType = empty;
            this.Earning = 0;
            this.Interval = 0;
            this.
        }

    }

    public enum FloorType
    {
        Empty = 1,
        SmallApartment = 2
    }

}