using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gearstore.Models
{
    public class cart
    {
        public Product product { get; set;  }
        public int quantity { get; set; }
        public string totalcost { get; set; }
    }

}