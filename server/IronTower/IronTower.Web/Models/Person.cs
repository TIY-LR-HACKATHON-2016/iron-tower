namespace IronTower.Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        public virtual Floor Home { get; set; }
        public virtual Floor Work { get; set; }
        public string Name { get; set; }

        public Person()
        {
            Name = Faker.NameFaker.Name();
        }
    }
}