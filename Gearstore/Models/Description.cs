using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gearproj.Models
{
 public   class Description
    {
        public int DescriptionId { get; set; }
        [ForeignKey("Prod")]
        public int ProdId { get; set; }
        
        public float Length { get; set; }
        public float Size { get; set; }
      [Required]
        public string PartNumber { get; set; }
        [Required]
        public string ModelFlexibility { get; set; }
        [Required]
        public string MoreDetails { get; set; }

        public int YearOfProduct { get; set; }
        [JsonIgnore]
        public Product Prod { get; set; }


    }
}
