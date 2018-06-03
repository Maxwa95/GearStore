using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gearproj.Models
{
    
    public class Brand
    {
        public int  BrandId { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string BrandName { get; set; }
        public string ImagePath { get; set; }
        [JsonIgnore]
        public virtual List<Model> models { get; set; } 

    }
}