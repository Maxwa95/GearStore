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


        //[HttpGet, Route("api/page")]
        //public IHttpActionResult Get(int Pagenum = 1, string Productname = "*", string Brandnames = "*", string Categories = "*")
        //{
        //    List<Product> result = new List<Product>();
        //    List<Product> result2 = new List<Product>();
        //    List<Product> result3 = new List<Product>();
        //    List<Product> result4 = new List<Product>();

        //    string[] bnames = Brandnames.Split(',');
        //    string[] cnames = Categories.Split(',');
        //    Brand b = new Brand();
        //    Categories c = new Categories();

        //    int pgn = Pagenum < 0 ? 1 : Pagenum > Math.Ceiling(db.products.Count() / 8.0) ? (int)Math.Ceiling(db.products.Count() / 8.0) : Pagenum;
        //    int count = db.products.Count() < pgn * 8 ? ((pgn - 1) * 8) : (pgn - 1) * 8;

        //    result = db.products.OrderByDescending(k => k.productId).Skip(count).Take(8).ToList();
        //    result2 = db.products.Where(a => a.ProductName.Contains(Productname)).Take(8).ToList();

        //    foreach (var item in bnames)
        //    {
        //        b = db.Brands.FirstOrDefault(a => a.BrandName == item);
        //        if (b != null)
        //        {
        //            result3.AddRange(db.products.Where(a => a.BrandId == b.BrandId).ToList());
        //        }
                

        //    }
        //    foreach (var item in cnames)
        //    {
        //        c = db.Categories.FirstOrDefault(a => a.CategoriesName == item);
        //        if (c != null)
        //        {
        //            result3.AddRange(db.products.Where(a => a.CategoryId == c.CategoriesId).ToList());
        //        }


        //    }



        //    if (result == null)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    if (result2.Capacity > 0 && result3.Capacity == 0 && result4.Capacity == 0)
        //    {
        //        return Ok(result2.Take(8).ToList());
        //    }
        //    else if (result3.Capacity > 0 && result4.Capacity == 0)
        //    {
        //        return Ok(result3);
        //    }
        //    else if (result3.Capacity > 0 && result4.Capacity > 0)
        //    {
               
        //        return Ok(result3.Where(a => a.CategoryId == c.CategoriesId && a.BrandId == b.BrandId).ToList());
        //    }
        //    else if (result4.Capacity > 0)
        //    {
        //        return Ok(result4);
        //    }
        //    else
        //    {
        //        return Ok(result);
        //    }

        //}

        [HttpGet, Route("api/ClientProducts")]
        public IHttpActionResult Get(int pagenum ,string name=null)
        {
            if (name == "*") name = null;
         int pgn = pagenum < 0 ? 1 :  pagenum > Math.Ceiling(db.products.Count() / 8.0) ?  (int)Math.Ceiling(db.products.Count() / 8.0) : pagenum ;
            int count = db.products.Count() < pgn*8 ? ((pgn-1) * 8 )  : (pgn-1)*8 ;
            var prods = db.products.Where(a=>a.ProductName==name||name==null).OrderByDescending(k => k.productId).Skip(count).Take(8).ToList();
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
            string[] brands = brandsname.Split(',');
            string[] catnames = catename.Split(',');
            var brandsid = db.Brands.Where(a => brands.Contains(a.BrandName)).Select(a => a.BrandId).ToList();
            var cats = db.Categories.Where(a => catnames.Contains(a.CategoriesName)).Select(a=>a.CategoriesId).ToList();
            int pgn = pagenum < 0 ? 1 : pagenum > Math.Ceiling(db.products.Count() / 8.0) ? (int)Math.Ceiling(db.products.Count() / 8.0) : pagenum;
            int count = db.products.Count() < pgn * 8 ? ((pgn - 1) * 8) : (pgn - 1) * 8;
            var prods = db.products.Where(a=>cats.Contains(a.CategoryId)|| brandsid.Contains(a.BrandId)||brandsid==null||catnames==null).OrderByDescending(k => k.productId).Skip(count).Take(8).ToList();
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
