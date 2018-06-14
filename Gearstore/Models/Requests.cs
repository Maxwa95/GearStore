using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gearstore.Models
{
    public class Requests
    {
        public int RequestsId { get; set; }
        [ForeignKey("need")]
        public int NeededId { get; set; }
        [ForeignKey("comp")]
        public int CompanyId { get; set; }
        
        public bool Received { get; set; }

        public NeededProducts need { get; set; }

        public Company comp { get; set; }

    }
}