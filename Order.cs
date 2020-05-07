using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurseWork
{
    public class Order
    {
        public int FoodId { get; set; }

        public int IdOrderList { get; set; }

        public int Id { get; set; }

        public decimal PriceBoughtFor { get; set; }

        public virtual Food Food { get; set; }

        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
