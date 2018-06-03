using gearproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gearproj.Controllers
{
    public class ModelsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IHttpActionResult get()
        {
            return Ok(db.Models.ToList());
        }
    }
}
