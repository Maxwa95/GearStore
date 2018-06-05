using gearproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace gearproj.Controllers
{
    
    public class SellerController : ApiController
    {

        ApplicationDbContext db = new ApplicationDbContext();


        // GET: api/Seller/5
        
        public IHttpActionResult Get(int id)
        {
            var c = db.Companies.FirstOrDefault(a => a.CompanyId == id);
            if (c == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(c);
            }

        }
      
        [HttpGet, Route("api/seller/GetProducts/{companyid:int}")]
        public IHttpActionResult GetProducts(int companyid)
        {
            var _var = db.products.Where(a => a.CompanyId == companyid).ToList();

            if (_var != null)
            {
                return Ok(_var);
            }
            else
                return BadRequest();
        }
        
        //// POST: api/Seller
        //[Authorize(Roles = "Seller")]
        //[HttpPost, Route("api/seller/product")]
        //public IHttpActionResult Post([FromBody]Product product)
        //{
           
        //    if (ModelState.IsValid)
        //    {
        //        db.products.Add(product);
        //        db.SaveChanges();
        //        return Ok(db.products.ToList());
        //    }
        //    else
        //        return BadRequest();

        //}
       
        [HttpPost, Route("api/seller/product")]
        public async Task<IHttpActionResult> Post([FromBody]Product product)
        {

            if (ModelState.IsValid)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                try
                { 
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                                
                            return BadRequest(message);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");
                                
                            return BadRequest(message);
                        }
                        else
                        {

                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Ok(message1); ;
                }
                var res = string.Format("Please Upload a image.");
                    return BadRequest(res);
                 }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                return BadRequest(res);
                }
        


        db.products.Add(product);
                db.SaveChanges();
                return Ok(db.products.ToList());
            }
            else
                return BadRequest();

        }


        // PUT: api/Seller/5
        [Authorize(Roles = "Seller")]
        [HttpPut, Route("api/seller/product")]
        public IHttpActionResult Put([FromBody]Product value)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(value).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok(db.products.ToList());
            }
            else
                return BadRequest();
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete, Route("api/seller/product/{Productid:int}")]
        public IHttpActionResult DeleteProd(int Productid)
        {
            var c = db.products.FirstOrDefault(a=> a.productId == Productid);
            if (c == null)
            {

                return BadRequest();
                
            }
            else
            {
                db.products.Remove(c);
                db.SaveChanges();
                return Ok(c);
            }

        }
    }
}
