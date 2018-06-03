using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gearproj.Models
{
    public class FeedBack
    {
        public int FeedBackId { get; set; }
        [MaxLength(255)]
        [Required]
        public string Comment { get; set; }
        [ForeignKey("user")]
        [Required]
        public string Userid { get; set; }
        [ForeignKey("prod")]
        [Required]
        public  int Productid { get; set; }
        
        public DateTime Date { get; set; }

        [JsonIgnore]
        public  Product prod { get; set; }
        [JsonIgnore]
        public ApplicationUser user { get; set; }
             
    }
}