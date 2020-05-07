using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurseWork
{
    internal class RequestIngredientsModel
    {
        internal int Id { set; get; }

        internal string Name { set; get; }

        internal decimal Count { set; get; }

        internal decimal AdditionalAmount { set; get; } 
    }
}
