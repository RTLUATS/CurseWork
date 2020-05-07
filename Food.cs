using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;

namespace CurseWork
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal CurrentPrice { get; set; }

        public string Recept { get; set; }

        public byte[] Image { get; set; }

        public bool? InMenu { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Structure> Structures { get; set; }

        public virtual Category Category { get; set; }
    }
}
