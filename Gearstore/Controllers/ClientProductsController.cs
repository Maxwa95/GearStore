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
        [HttpGet, Route("api/ClientProducts/byname")]
        public IHttpActionResult Get(int pagenum ,string name=null)
        {
            if (name == "*") { name = null; }
         int pgn = pagenum < 0 ? 1 :  pagenum > Math.Ceiling(db.products.Count() / 9.0) ?  (int)Math.Ceiling(db.products.Count() / 9.0) : pagenum ;
            int count = db.products.Count() < pgn*9 ? ((pgn-1) * 9 )  : (pgn-1)*9 ;
            var prods = db.products.Where(a=>a.ProductName==name||name==null).OrderByDescending(k => k.productId).Skip(count).Take(9).ToList();
            if (prods == null)
            {
                return BadRequest();
            }
            else
             return Ok(prods);
        }
        [HttpGet, Route("api/filterClientProducts")]
        public IHttpActionResult Get(int pagenum,string catename="",string  brandsname="")
        {

            //if (catename == null)
            //{
            //    catename = "*";
            //}
           
            string[] brands = brandsname.Split(',');
            string[] catnames = catename.Split(',');
            var brandsid = db.Brands.Where(a => brands.Contains(a.BrandName)).Select(a => a.BrandId).ToList();
            var cats = db.Categories.Where(a => catnames.Contains(a.CategoriesName)).Select(a=>a.CategoriesId).ToList();
            int pgn = pagenum < 0 ? 1 : pagenum > Math.Ceiling(db.products.Count() / 8.0) ? (int)Math.Ceiling(db.products.Count() / 8.0) : pagenum;
            int count = db.products.Count() < pgn * 8 ? ((pgn - 1) * 8) : (pgn - 1) * 8;
            var prods = db.products.Where(a=>cats.Contains(a.CategoryId)|| brandsid.Contains(a.BrandId)||brandsid.Count==0&& cats.Count==0).OrderByDescending(k => k.productId).Skip(count).Take(8).ToList();
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
            }).OrderByDescending(k => k.count).Take(6).ToList();
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


        [HttpGet,Route("api/clientproducts/last")]
        public IHttpActionResult Lastprods()
        {
            var lastprods = db.products.OrderByDescending(a => a.productId).Take(6).ToList();
            if (lastprods == null)
            {
                return BadRequest();
            }
            else
                return Ok(lastprods);
        }

        [HttpGet, Route("api/clientproducts/offers")]
        public IHttpActionResult offers()
        {
            var disprods = db.products.OrderByDescending(a => a.Discount).Take(5).ToList();
            if (disprods == null)
            {
                return BadRequest();
            }
            else
                return Ok(disprods);
        }



        [HttpGet]
        public IHttpActionResult Getprod(int id)
        {
            var res = db.products.FirstOrDefault(a => a.productId == id);
            var compname = db.Companies.FirstOrDefault(a => a.CompanyId == res.CompanyId);
            var others = db.products.Where(a => a.CategoryId == res.CategoryId && a.productId != res.productId).Take(3).ToList();
            if (res == null)
            {
                return BadRequest();
            }
            //hi
            else
            return Ok(new {res,compname,others});
        }



    }
}
