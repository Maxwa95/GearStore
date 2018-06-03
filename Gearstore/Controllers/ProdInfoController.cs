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
            var product = db.products.FirstOrDefault(a => a.productId == id);
            //   var f = db.Feedbacks.Where(a => a.Productid == id).ToList();
            var otherproducts = db.products.Where(a => a.CategoryId == product.CategoryId && a.productId != product.productId).Take(3).ToList();
            var comments = db.Users.Join(db.Feedbacks, u => u.Id, feed => feed.Userid, (u, feed) => new
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Comment = feed.Comment,
                Productid = feed.Productid
            }

                ).Where(a => a.Productid == id);  // your starting point - table in the "from" statement

            if (product == null||otherproducts==null ||comments==null )
            {
                return BadRequest();
            }
            else
                return Ok(new { product, otherproducts, comments });
        }
        
    }
}
