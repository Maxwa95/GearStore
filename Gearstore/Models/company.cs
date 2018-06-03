using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace gearproj.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(255)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required]
        public string HomePhone { get; set; }
        public int? view { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; } 
        
        public float Rate { get; set; }
        [ForeignKey("user")]
        public string userid { get; set; }
        [JsonIgnore]
        public ApplicationUser user { get; set; }

       public Company()
        {
            Rate = 0.0F;
        }
    }
}