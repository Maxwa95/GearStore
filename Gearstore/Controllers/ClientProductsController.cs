using gearproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gearproj.Controllers
{
    

    public class ClientProductsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public IHttpActionResult Get(int pagenum)
        {
         int pgn = pagenum < 0 ? 1 :  pagenum > Math.Ceiling(db.products.Count() / 8.0) ?  (int)Math.Ceiling(db.products.Count() / 8.0) : pagenum ;
            int count = db.products.Count() < pgn*8 ? ((pgn-1) * 8 )  : (pgn-1)*8 ;
            var prods = db.products.OrderByDescending(k => k.productId).Skip(count).Take(8).ToList();
            if (prods == null)
            {
                return BadRequest();
            }
            else
             return Ok(prods);
        }
        

        //get top selling products

        public IHttpActionResult Gettopselling()
        {
            List<Product> prods = new List<Product>();
            var bestproducts = db.OrderDetails.GroupBy(i => i.productId).Select(g => new
            {
               count = g.Count(),
                id = g.Key
            }).OrderByDescending(k => k.count).Take(4).ToList();
            if (bestproducts == null)
            {
                return BadRequest();
            }
            else
                foreach (var item in bestproducts)
                {
                    prods.Add(db.products.FirstOrDefault(a => a.productId == item.id));
                }

            return Ok(prods);
        }

   
      
        [HttpGet]
        public IHttpActionResult Getprod(int id)
        {
            var res = db.products.FirstOrDefault(a => a.productId == id);
            var others = db.products.Where(a => a.CategoryId == res.CategoryId && a.productId != res.productId).Take(3).ToList();
           
            if (res == null)
            {
                return BadRequest();
            }
            else
            return Ok(new {res,others});
        }



    }
}
