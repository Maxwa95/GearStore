using gearproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gearproj.Controllers
{
   
    public class ProductsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Feedbacks/5
   
        public IHttpActionResult Get(int id)
        {
           var p = db.products.FirstOrDefault(a => a.productId == id);
            var f = db.Feedbacks.Where(a => a.Productid == id).ToList();
            var others = db.products.Where(a => a.CategoryId == p.CategoryId && a.productId != p.productId).Take(3).ToList();
            if (p == null)
            {
                return BadRequest();
            }else
            return Ok(new { p, others,f });
        }
        
    }
}
