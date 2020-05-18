using System;
using System.Collections.Generic;

namespace CurseWork
{
    public class OrderList
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public decimal AmountOrder { get; set; }

        public DateTime DateOrder { get; set; }
        
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public  string LastName { get; set; }

        public string Telephone { get; set; }

        public virtual  User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
