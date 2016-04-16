using System.ComponentModel.DataAnnotations.Schema;

namespace IronTower.Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        [ForeignKey("Home")]
        public int HomeId { get; set; }
        public virtual Floor Home { get; set; }
        [ForeignKey("Work")]
        public int? WorkId { get; set; }
        public virtual Floor Work { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }

        public Person()
        {
            Name = Faker.NameFaker.Name();
        }
    }
}