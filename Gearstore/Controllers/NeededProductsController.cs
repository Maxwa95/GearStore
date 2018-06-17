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
using Gearstore.Models;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Http.Cors;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Gearstore.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [Route("api/NeededProducts/{productname:alpha}")]
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
        [HttpPost,Authorize(Roles ="Client")]
        [Route("api/need")]
        public IHttpActionResult PostNeededProducts()
        {
            NeededProducts np = new NeededProducts();
            var httpRequest = HttpContext.Current.Request;
            np.FullName = httpRequest.Form["FullName"];
            np.TextResponce = httpRequest.Form["TextResponce"];
            np.userid =  User.Identity.GetUserId();
            np.RequestDate = DateTime.Now;
            np.StatuseResponce = "Pending";
            if (httpRequest.Files.Count == 1 && np.FullName != null && np.TextResponce != null)
            {
                if (httpRequest.Files[0].ContentType == "image/jpeg" && httpRequest.Files[0].ContentLength < 2048000000)
                {
                    var name = Guid.NewGuid().ToString().Split('-')[0] + ".jpg";
                    var postedFile = httpRequest.Files[0];
                    var filePath = HttpContext.Current.Server.MapPath("~/Content/NeedImages/" + name );
                    np.ImagePath = name;
                    postedFile.SaveAs(filePath);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
            db.NeededProducts.Add(np);
            db.SaveChanges();
            return Ok();
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