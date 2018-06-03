using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gearproj.Models
{
    public class modelsproducts
    {
        public int id { get; set; }
        [ForeignKey("product")]
        public int productId { get; set; }


        [ForeignKey("model")]
        public int modelId { get; set; }

        public Product product { get; set; }

        public Model model { get; set; }

    }
}