using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using gearproj.Models;

namespace gearproj.Controllers
{
  
    public class FeedbackController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
       
        [Authorize(Roles = "Client")]
        // POST: api/Feedback
        public IHttpActionResult Post(FeedBack fb)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(fb);
                db.SaveChanges();
                return Ok(db.Feedbacks.ToList());

            }
            else
            {
                return BadRequest();
            }
        }

    }
}
