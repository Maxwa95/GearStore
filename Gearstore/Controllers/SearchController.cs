using gearproj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gearproj.Controllers
{
    public class SearchController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet, Route("api/Search/{key:alpha}")]
        // GET: api/Search/{key:string}
        public IHttpActionResult GetByCharacter(string key)
        {
            Categories cat = db.Categories.FirstOrDefault(a=>a.CategoriesName.Contains(key));

            var ProductsResult = db.products.Where(a=>a.ProductName.Contains(key)).Select(a=>new {ProductName=a.ProductName }).Take(4).ToList();

            List<Product> catproductResult = db.products.Where(a=>a.CategoryId == cat.CategoriesId).Take(3).ToList();

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
            return Ok(new { catid = cat.CategoriesId,catname = cat.CategoriesName , productsincat = catproductResult , productsmatch =  ProductsResult });
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
