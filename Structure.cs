using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurseWork
{
    public class Structure
    {
        [Key, Column(Order = 1)]
        public int FoodId { get; set; }

        [Key, Column(Order = 2)]
        public int IngredientId { get; set; }

        public decimal  Quantity { get; set; }

        public string CookingStep { get; set; }

        public virtual Food Food { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
