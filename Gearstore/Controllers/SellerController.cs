﻿using Gearstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Data;
using System.Web.Security;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace Gearstore.Controllers
{
    
    public class SellerController : ApiController
    {

        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        [Authorize(Roles = "Seller")]
        [Route("api/whoami")]
        public IHttpActionResult whoami()
        {
            return Ok(new { Type = "Seller" });
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        [Route("api/whoami/client")]
        public IHttpActionResult whoamiuser()
        {
            return Ok(new { Type = "Client" });
        }


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
        [Authorize(Roles = "Seller")]
        [HttpGet, Route("api/seller/GetProducts")]
        public IHttpActionResult GetProducts()
        {

            string userid = User.Identity.GetUserId();
            var _var = db.products.Where(a => a.CompanyId == db.Companies.FirstOrDefault(x => x.userid == userid).CompanyId).ToList();
            
            var result = _var.Select(a => new
            {
                Product = a,
                Brands = a.getbrand().FirstOrDefault(),
                Models = a.getdesc().FirstOrDefault()

            }).ToList();
                
            
            if (_var != null)
            {
                return Ok(
                result
                    );
            }
            else
                return BadRequest();
        }

        //// POST: api/Seller
       [Authorize(Roles ="Seller")]
        [HttpPost, Route("api/seller/product")]
        public IHttpActionResult Post([FromBody]Productdesc productdesc)
        {
            int Result;
            string extension,myimage;
            int imagenumber=1;
            Image productimage = new Image();
            var userid = User.Identity.GetUserId();
            var c = db.Companies.FirstOrDefault(a => a.userid == userid);

            if (ModelState.IsValid)
            {
                productdesc.product.CompanyId = c.CompanyId;
                db.products.Add(productdesc.product);
                db.SaveChanges();
                productdesc.Desc.ProdId = productdesc.product.productId;
                db.Descriptions.Add(productdesc.Desc);
                Result = db.SaveChanges();
               
                    if(Result!=1)
                    return BadRequest("not valid image");


                return Ok(new { id = productdesc.product.productId });



            }
            else
                return BadRequest();

        }
        [Authorize(Roles ="Seller")]
        [HttpPost, Route("api/seller/productImages")]
        public  IHttpActionResult PostImages()
        {
            var httpRequest = HttpContext.Current.Request;
           int productId = int.Parse(httpRequest.Form["prodid"]);
          if (ModelState.IsValid)
            {
                List<Image> productimages = new List<Image>();
                try
                {

                    int imagenumber = 1;
                    foreach (string imgname in httpRequest.Files)
                    {

                        HttpPostedFile img = httpRequest.Files[imgname];
                        Image image = new Image();
                        string extension = img.FileName.Substring(img.FileName.LastIndexOf("."));

                        image.ImgUrl = productId.ToString() + imagenumber.ToString() + extension;
                        string myimage = image.ImgUrl;
                        image.ProductId = productId;
                        db.images.Add(image);
                        int Result = db.SaveChanges();
                        if (Result == 1)
                            img.SaveAs(HostingEnvironment.MapPath("~/Content/ProdImages/") + myimage);
                        else break;
                        imagenumber++;
                    }
                    imagenumber = 1;
                    return Ok();

                    //    var httpRequest = HttpContext.Current.Request;
                    //    foreach (string file in httpRequest.Files)
                    //    {
                    //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    //        var postedFile = httpRequest.Files[file];
                    //        if (postedFile != null && postedFile.ContentLength > 0)
                    //        {

                    //            int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                    //            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                    //            var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                    //            var FileName = postedFile.FileName.Replace(ext,"");
                    //            var extension = ext.ToLower();
                    //            if (!AllowedFileExtensions.Contains(extension))
                    //            {

                    //                var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                    //                return BadRequest(message);
                    //            }
                    //            else if (postedFile.ContentLength > MaxContentLength)
                    //            {

                    //                var message = string.Format("Please Upload a file upto 1 mb.");

                    //                return BadRequest(message);
                    //            }
                    //            else
                    //            {

                    //                var filePath = HttpContext.Current.Server.MapPath("~/Content/ProductImages/" + FileName + extension);
                    //                postedFile.SaveAs(filePath);
                    //                Image img = new Image();
                    //                img.ImgUrl = FileName + extension;
                    //                img.ProductId = prodId;
                    //                productimages.Add(img);


                    //            }
                    //        }
                    //    }
                    //    if (productimages.Count > 0)
                    //    {

                    //        var message1 = string.Format("Images Uploaded Successfully.");

                    //        db.images.AddRange(productimages);
                    //        db.SaveChanges();
                    //        return Ok(message1); ;
                    //    }
                    //    else
                    //    {
                    //        var res = string.Format("Please Upload a image.");
                    //        return BadRequest(res);
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                   

                    return BadRequest(ex.Message);
                }
                }
            else
                return BadRequest();

        }


        // PUT: api/Seller/5
        [Authorize(Roles = "Seller")]
        [HttpPut, Route("api/seller/product")]
        public IHttpActionResult Put([FromBody]Product product , HttpPostedFileBase [] images)
        {
            int Result;
            string extension, myimage;
            int imagenumber = 1;
            List<Image> productimage = new List<Image>(); 

            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                     Result=db.SaveChanges();
                if (Result == 1)
                {
                    
                    foreach (var img in images)
                    {
                        extension = img.FileName.Substring(img.FileName.LastIndexOf("."));
                myimage= product.productId + imagenumber + extension;
                        img.SaveAs(HostingEnvironment.MapPath("~/Content/ProductImages/" + myimage));
                        imagenumber++;
                    }
                    imagenumber = 1;
                }
                return Ok();
                
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
                db.images.RemoveRange(db.images.Where(a => a.ProductId == Productid));

               // db.SaveChanges();
                return Ok(c);
            }

        }
    }
}
