using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gearstore.Models
{
    public class Checkout
    {
        public cart[] cart { get; set; }
        public float total { get; set; }
        
    }
}