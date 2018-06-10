using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using gearproj.Models;

namespace Gearstore.Controllers
{
    public class NeededProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/NeededProducts
        public IQueryable<NeededProducts> GetNeededProducts()
        {
            return db.NeededProducts;
        }

        // GET: api/NeededProducts/5
        [ResponseType(typeof(NeededProducts))]
        public IHttpActionResult GetNeededProducts(int id)
        {
            NeededProducts neededProducts = db.NeededProducts.Find(id);
            if (neededProducts == null)
            {
                return NotFound();
            }

            return Ok(neededProducts);
        }
        // GET: api/NeededProducts/5
        [Route("/NeededProducts/{productname:alpha}")]
        public IHttpActionResult GetNeededProducts(int pagenum,string productname )
        {
            
            List<Product> neededProducts = db.products.Where(a => a.ProductName == productname).ToList();
            var brandsid = db.Brands.Where(a => productname.Contains(a.BrandName)).Select(a => a.BrandId).ToList();
            var modelsbrandid= db.Models.Where(a => productname.Contains(a.ModelName)).Select(a => a.BrandId).ToList();
            int pgn = pagenum < 0 ? 1 : pagenum > Math.Ceiling(db.products.Count() / 8.0) ? (int)Math.Ceiling(db.products.Count() / 8.0) : pagenum;
            int count = db.products.Count() < pgn * 8 ? ((pgn - 1) * 8) : (pgn - 1) * 8;
            List<Product> prodsByBrandModel = new List<Product>();
            if (brandsid.Count != 0 || modelsbrandid.Count != 0)
            {
                prodsByBrandModel = db.products.Where(a => modelsbrandid.Contains(a.BrandId) || brandsid.Contains(a.BrandId)).OrderByDescending(k => k.productId).Skip(count).Take(8).ToList();
            }
            neededProducts.AddRange(prodsByBrandModel);
            if (neededProducts == null)
            {
                NeededProducts needyproduct = new NeededProducts();
                needyproduct.FullName = productname;
                needyproduct.RequestDate = DateTime.Now;
                needyproduct.StatuseResponce = "Pending";
                needyproduct.TextResponce = "Your Request for This Product is Proceesing now ....";
                needyproduct.ImagePath = "";
                return Ok(needyproduct.TextResponce);
            }

            return Ok(neededProducts);
        }

        // PUT: api/NeededProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNeededProducts(int id, NeededProducts neededProducts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != neededProducts.NeededProductsId)
            {
                return BadRequest();
            }

            db.Entry(neededProducts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeededProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/NeededProducts
        [ResponseType(typeof(NeededProducts))]
        public IHttpActionResult PostNeededProducts(NeededProducts neededProducts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NeededProducts.Add(neededProducts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = neededProducts.NeededProductsId }, neededProducts);
        }

        // DELETE: api/NeededProducts/5
        [ResponseType(typeof(NeededProducts))]
        public IHttpActionResult DeleteNeededProducts(int id)
        {
            NeededProducts neededProducts = db.NeededProducts.Find(id);
            if (neededProducts == null)
            {
                return NotFound();
            }

            db.NeededProducts.Remove(neededProducts);
            db.SaveChanges();

            return Ok(neededProducts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NeededProductsExists(int id)
        {
            return db.NeededProducts.Count(e => e.NeededProductsId == id) > 0;
        }
    }
}