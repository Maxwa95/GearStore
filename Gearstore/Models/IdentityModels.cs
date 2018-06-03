using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace gearproj.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string usertype { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string profileImage { get; set; }

        public virtual List<FeedBack> feedbacks { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            
                                               // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<modelsproducts> modelsproducts { get; set; }
      

        public DbSet<Company> Companies { get; set; }

        public DbSet<Description> Descriptions { get; set; }

        public DbSet<Image> images { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<NeededProducts> NeededProducts { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ShippingCompany> ShippingCompanies { get; set; }

        public DbSet<FeedBack> Feedbacks { get; set; }
        public DbSet<SimilaritiesProducts>  SimilaritiesProducts { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
