using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Data.Entity; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gearproj.Models
 { 
    public class SimilaritiesProducts
    {
        public int SimilaritiesProductsId { get; set; }
    
        //relations
        
        [ForeignKey("needyproduct")]
        public int NeededProductsId { get; set; }
        [JsonIgnore]
        public NeededProducts needyproduct {get;set;}
        [ForeignKey("pro")]
        public int productId { get; set; }
        [JsonIgnore]
        public Product pro { get; set; }
    } 
            
}