using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurseWork
{
    public class Inquiry
    {
        public int Id { get;set; }

        public int IngredientId { get; set; }

        public DateTime Date { get; set; }

        public decimal ExpectedQuantity { get; set; }

        public virtual Ingredient Ingredient { get; set; }

    }
}
