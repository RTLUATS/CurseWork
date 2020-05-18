using System.Collections.Generic;

namespace CurseWork
{
    public class User
    {
        public int Id { get; set;}
        
        public string Login { get; set; }
        
        public string Password { get; set; }

        public int LvlAccess { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Telephone { get; set; }

        public bool IsBlock { get; set; }

        public virtual ICollection<OrderList> OrderLists { get; set; }

    }
}
