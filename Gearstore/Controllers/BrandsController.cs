﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gearstore.Models;
using gearproj.Models;

namespace gearproj.Controllers
{
    
    public class BrandsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Brands
        // get brands infos
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,db.Brands.Select(i => new { i.BrandName,i.ImagePath }).ToList());
        }
       [Route("api/getfilterdata")]
        public IHttpActionResult Getfilterdata()
        {
            //  return Request.CreateResponse(HttpStatusCode.OK, db.Brands.Select(i => new { i.BrandName, i.BrandId }).ToList());
           return Ok(new { Brands =  db.Brands.ToList() , Categories  = db.Categories.ToList() });

        }




    }
}
