using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Gearstore.Models;

namespace Gearstore.Controllers
{
    
    public class BrandsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Brands
        // get brands infos
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Brands.ToList());
        }
        [Route("api/categories")]
        public IHttpActionResult Getcats()
        {
            var c = db.Categories.ToList();
            if (c == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(c);
            }

        }
        [Route("api/models")]
        public IHttpActionResult Getmods()
        {
            var c = db.Models.ToList();
            if (c == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(c);
            }
        }


        [Route("api/getfilterdata")]
        public IHttpActionResult Getfilterdata()
        {
            //  return Request.CreateResponse(HttpStatusCode.OK, db.Brands.Select(i => new { i.BrandName, i.BrandId }).ToList());
           return Ok(new { Brands =  db.Brands.ToList() , Categories  = db.Categories.ToList() });

        }




    }
}
