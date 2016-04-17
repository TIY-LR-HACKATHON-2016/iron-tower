using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices.Internal;

namespace IronTower.Web.Models
{

    public enum FloorCategory
    {
        Empty,
        Apartment,
        Business ,
    }

    public class FloorType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FloorCategory Category { get; set; }
        [NotMapped]
        public bool IsApartment => Category == FloorCategory.Apartment;
        public int Earning { get; set; } // set by type and modifier

        public int PeopleLimit { get; set; } //set by type

        public int EarningIncrease { get; set; }//per new employee set by type
        public int BuildCost { get; set; } //set by type
    }
    public class Floor
    {
     
        public int Id { get; set; }
       
        [NotMapped]
        public int NumPeople => this.People.Count;

        public virtual FloorType FloorType { get; set; }

        [Required]
        public Game Game { get; set; }

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
       
    }
}