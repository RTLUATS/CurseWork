using MSharp.Framework.Services.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

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

        public virtual ICollection<Inquiry> Inquiries { get; set; }

    }
}
