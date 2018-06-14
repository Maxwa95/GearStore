using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gearstore.Models
{
    public class Model
    {
        public int ModelId { get; set; }
        [Required]
        [StringLength(255,MinimumLength =100)]
        public string ModelName { get; set; }
        [ForeignKey("brand")]
        public int? BrandId { get; set; }
        public Brand brand { get; set; }
        [JsonIgnore]
        public virtual List<Categories_Model> categories { get; set; }

/*
        public virtual NeededProducts needproduct { get; set; }

        [JsonIgnore]
        //*/
        //[JsonIgnore]
        //public virtual List<Product> products { get; set; }
    }
}