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
    public class OrderInfo 
    {
        public int OrderInfoId { get; set; }
        [Required]
        [JsonIgnore]
        public DateTime OrderDate { get; set; }
        [Required]
        public string TotalCost { get; set; }
        [Required]
        public string SelectedAdd { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        [JsonIgnore]
        public virtual List<OrderDetails> Products { get; set; }

        // relations 
        [ForeignKey("user")]
        public string userid { get; set; }
        [JsonIgnore]
        public ApplicationUser user { get; set; }

        [ForeignKey("ShippingCompany")]
        public int ShippingCompanyid { get; set; }
        [JsonIgnore]
        public ShippingCompany ShippingCompany { get; set; }
           
            } 
            
            }