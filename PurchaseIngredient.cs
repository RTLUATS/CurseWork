using System;

namespace CurseWork
{
    public class PurchaseIngredient
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        
    }
}
