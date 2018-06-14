using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Gearstore.Models
{
    public class Product
    {

        public int productId { get; set; }
        [Required]
        [StringLength(255,MinimumLength = 2)]   
        public string ProductName { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 2)]
        public string PlaceOfOrigin { get; set; }
        public string state { get; set; }
       
        public string Manufacturer { get; set; }
        [Required]
        public DateTime DateOfPublish { get; set; }
        
        public virtual List<Image> Imgs { get; set; }
        [Required]
        public float Price { get; set; }
        public float Discount { get; set; }
        
        public int Quantity { get; set; }
        [ForeignKey("cpy")]
        public int CompanyId { get; set; }
        [ForeignKey("cats")]
        public int CategoryId { get; set; }
        public int Rate { get; set; }
        [JsonIgnore]
        public Company cpy { get; set; }
        [JsonIgnore]
        public Categories cats { get; set; }
        [JsonIgnore]
        public virtual List<FeedBack> feeds { get; set; }


        [ForeignKey("bra")]
        public int BrandId { get; set; }
         

        public Brand bra { get; set; }
        
        [JsonIgnore]
        public virtual List<OrderDetails> Orders { get; set; }
       

        public List<Description> getdesc()
        {
            var Result = new List<Description>();
            ApplicationDbContext db = new ApplicationDbContext();
            var choice = db.Descriptions.FirstOrDefault(a => a.ProdId == this.productId);
            if (choice != null)
            {
                var ALL = db.Descriptions.Where(a => a.ProdId != choice.ProdId).ToList();
                Result.Add(choice);
                Result.AddRange(ALL);
            }
            else
            {
                Result = new List<Description>();
            }
            return Result;
        }
        public List<Categories> getcat()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Result = new List<Categories>();
            var choice = db.Categories.FirstOrDefault(a => a.CategoriesId == this.CategoryId);
            var ALL = db.Categories.Where(a => a.CategoriesId != choice.CategoriesId).ToList();
            Result.Add(choice);
            Result.AddRange(ALL);
            return Result;
        }
        public List<Brand> getbrand()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Result = new List<Brand>();
            var choice = db.Brands.FirstOrDefault(a => a.BrandId == this.BrandId);
            var ALL = db.Brands.Where(a => a.BrandId != choice.BrandId).ToList();
            Result.Add(choice);
            Result.AddRange(ALL);
            return Result;
        }

        public List<Company> getcomp()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var Result = new List<Company>();
            var choice = db.Companies.FirstOrDefault(a => a.CompanyId == this.CompanyId);
            var ALL = db.Companies.Where(a => a.CompanyId != choice.CompanyId).ToList();
            Result.Add(choice);
            Result.AddRange(ALL);
            return Result;
        }


    }
}
