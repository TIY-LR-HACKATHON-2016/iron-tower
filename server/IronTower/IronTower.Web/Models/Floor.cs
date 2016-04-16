using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;

namespace IronTower.Web.Models
{
    public class Floor
    {
     
        public int Id { get; set; }
        public bool isApartment { get; set; } = false; // set by type
        public int Earning { get; set; } // set by type and modifier
        public int PeopleLimit { get; set; } //set by type
        public int NumPeople { get; set; }
        public int FloorType{ get; set; }

        //game balance
        public int EarningIncrease { get; set; }//per new employee set by type
        public int BuildCost { get; set; } //set by type

        public virtual ICollection<Person> People { get; set; } = new List<Person>();

        //public Floor(Type number)
        //{
        //  FloorType = number;
        //  isApartment
        //  Earning
        //  EarningIncrease
        //  PeopleLimit
        //  BuildCost
        //}
        public Floor()
        {

        }

        public Floor(int type)
        {
            switch(type)
            {
                case 0:
                    break;
                case 1:
                    FloorType = 1;
                    isApartment = true;
                    PeopleLimit = 2;
                    BuildCost = 200;
                    break;
                case 2:
                    FloorType = 2;
                    isApartment = false;
                    Earning = 25;
                    EarningIncrease = 2;
                    PeopleLimit = 2;
                    BuildCost = 200;
                    break;
            }
        }
        
    }

    /*Floor types
     * 0 - empty
     * 1 - small apartment
     * 2 - cafe
    */

}