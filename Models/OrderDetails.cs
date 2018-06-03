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
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        [Required] 
        public int Quantity { get; set; }
        [Required]
        public float currentprice { get; set; }
        // relations 
        [ForeignKey("order")]
        public int OrderId { get; set; }
        [JsonIgnore]
        public OrderInfo order { get; set; }

        [ForeignKey("pro")]
        public int productId { get; set; }
        [JsonIgnore]
        public Product pro { get; set; }
    } 
            
}