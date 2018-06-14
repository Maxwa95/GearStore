using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gearstore.Models
{
 public class Description
    {
        public int DescriptionId { get; set; }
        [ForeignKey("Prod")]
        public int ProdId { get; set; }
        
        public float? Length { get; set; }
        public float? Size { get; set; }
      [Required]
        public string PartNumber { get; set; }
        [Required]
        public string ModelFlexibility { get; set; }
        [Required]
        public string MoreDetails { get; set; }

<<<<<<< HEAD
        public DateTime YearOfProduct { get; set; }
        [JsonIgnore]
=======
        public int YearOfProduct { get; set; }
      [JsonIgnore]
>>>>>>> 9578db3e4bfa067133b7779da05a4f26a55f025d
        public Product Prod { get; set; }


    }
}
