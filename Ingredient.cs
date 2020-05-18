using System.Collections.Generic;

namespace CurseWork
{
    public  class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Count { get; set; }

        public decimal Price { get; set; }

        public string Unit { get; set; }
        
        public virtual ICollection<Structure> Structures { get; set; }

        public virtual ICollection<PurchaseIngredient> PurchaseIngredients { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}
