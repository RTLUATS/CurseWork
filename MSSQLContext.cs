using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CurseWork
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext() : base("MSSQL") { }

        public DbSet<User> Users { get; set; }

        public DbSet<OrderList> OrderLists { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Structure> Structures { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<PurchaseIngredient> PurchaseIngredients { get; set; }

        public DbSet<Inquiry> Inquiries { get; set; }
    }
}
