using System.Collections.Generic;


namespace CurseWork
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}
