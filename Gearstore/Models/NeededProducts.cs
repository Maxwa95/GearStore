using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Data.Entity; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Gearstore.Models
 { 
    public class NeededProducts
    {
        public int NeededProductsId { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public string FullName { get; set; }
        
        public DateTime RequestDate{get;set;}
        
        public string StatuseResponce{get;set;} //pending //closed // invalid request
        
        public string TextResponce {get;set;}
        [ForeignKey("user")]
        public string userid { get; set; }
       [JsonIgnore]
        public virtual ApplicationUser user { get; set; }

    } 
}