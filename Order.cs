using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork
{
    public class Order
    {
        [Key, Column(Order = 1)]
        public int FoodId { get; set; }

        [Key, Column(Order = 2)]
        public int OrderListId { get; set; }

        public decimal PriceBoughtFor { get; set; }

        public int Count { get; set; }

        public virtual Food Food { get; set; }

        public virtual OrderList OrderList { get; set; }
    }
}
