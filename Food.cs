using System.Collections.Generic;

namespace CurseWork
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal CurrentPrice { get; set; }

        public byte[] Image { get; set; }

        public bool? InMenu { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        
        public virtual ICollection<Structure> Structures { get; set; }

        public virtual Category Category { get; set; }
    }
}
