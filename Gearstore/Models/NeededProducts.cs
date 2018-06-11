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
        [Required]
        public DateTime RequestDate{get;set;}

        [Required]
        public string StatuseResponce{get;set;} //pending //closed // invalid request
        
        public string TextResponce {get;set;}
        [JsonIgnore]
        public virtual List<SimilaritiesProducts> Products { get; set; }
        [ForeignKey("model")]
        public int modelid { get; set; }
        [ForeignKey("brand")]
        public int brandid { get; set; }
        public Brand brand { get; set; }
        public Model model { get; set; }

    } 
            
}