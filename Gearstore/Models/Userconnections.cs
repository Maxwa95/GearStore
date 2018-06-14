using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Gearstore.Models
{
    public class Userconnections
    {
        public string UserconnectionsId { get; set; }

        [ForeignKey("user")]
        public string UserId { get; set; }

        public bool Connected { get; set; }

        public ApplicationUser user { get; set; }
    }
}