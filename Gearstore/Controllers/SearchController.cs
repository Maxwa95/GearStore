﻿using Gearstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gearstore.Controllers
{
    public class SearchController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet, Route("api/Search/{key}")]
        // GET: api/Search/{key:string}
        public IHttpActionResult GetByCharacter(string key)
        {
            List<int> cat = db.Categories.Where(a => a.CategoriesName.Contains(key)).Select(a=> a.CategoriesId ).ToList();

            var ProductsResult = db.products.Where(a=>a.ProductName.Contains(key)).Select(a=> a.ProductName).Take(4).ToList();

            //var catproductResult = db.products.Where(a=> cat.Contains(a.CategoryId)).Select(a=> a.ProductName).Take(3).ToList();

            //List<Brand> BrandsResult = db.Brands.Where(a => a.BrandName.Contains(key)).ToList<Brand>();
            //if(BrandsResult !=null)
            //{
            //    foreach(var bd in BrandsResult)
            //    {
            //        var productsperbrand = db.products.Where(a => a.BrandId == bd.BrandId).Select(a => new { ProductName = a.ProductName }).ToList();
            //        ProductsResult.AddRange(productsperbrand);
            //    }
            //}
            //if (ModelsResult != null)
            //{
            //    foreach (var bm in ModelsResult)
            //    {
            //        List<modelsproducts> Modelproducts = db.modelsproducts.Where(a => a.modelId == bm.ModelId).ToList<modelsproducts>();
            //        if (Modelproducts != null)
            //        {
            //            foreach (var mpobj in Modelproducts)
            //            {
            //                var productsperModel = db.products.Where(a => a.productId == mpobj.productId).Select(a => new { ProductName = a.ProductName }).ToList();
            //                ProductsResult.AddRange(productsperModel);
            //            }
            //        }
            //    }
            //}
            return Ok(new { catsmatch = db.Categories.Where(a=> cat.Contains(a.CategoriesId)).ToList() , productsmatch =  ProductsResult });
        }
        [HttpGet, Route("api/Getproductsbyname/{name:alpha}")]
        public IHttpActionResult Getproductsbyname(string name)
        {
     var products = db.products.Where(a => a.ProductName == name).ToList();
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }
        [HttpGet, Route("api/Getproductsbynameandcat/{name:alpha}/{catid:int}")]
        public IHttpActionResult Getproductsbynameandcat(string name ,int catid)
        {
            var products = db.products.Where(a => a.ProductName == name && a.CategoryId==catid).ToList();
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }

    }
}
