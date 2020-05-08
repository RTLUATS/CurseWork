using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurseWork
{
    public class Structure
    {
        [Key, Column(Order = 1)]
        public int FoodId { get; set; }

        [Key, Column(Order = 2)]
        public int IngredientId { get; set; }

        public decimal  Quntity { get; set; }

        public string CookingStep { get; set; }

        public virtual ICollection<Food> Foods { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
