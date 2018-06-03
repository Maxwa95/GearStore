using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gearproj.Models
{
   public class Image
    {
        public int ImageId { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [ForeignKey("Pro")]
        public int ProductId { get; set; }
        [JsonIgnore]

        public Product Pro { get; set; }

    }
}
