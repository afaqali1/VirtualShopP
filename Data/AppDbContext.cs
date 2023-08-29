using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtualShop.Models;

namespace VirtualShop.Data
{
    public class AppDbContext : DbContext
    {
        private IHttpContextAccessor _contextAccessor;
        private AppUserViewModel LoggedInUser { get; set; }
        public AppDbContext (DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
            GetLoggedInUser();
        }

        public DbSet<VirtualShop.Models.Category> Categories { get; set; } = default!;
        public DbSet<VirtualShop.Models.AppUser> AppUsers { get; set; } = default!;
        public DbSet<VirtualShop.Models.Cart> Carts { get; set; } = default!;
        public DbSet<VirtualShop.Models.Product> Products { get; set; } = default!;
        public DbSet<VirtualShop.Models.Wishlist> Whishlists { get; set; } = default!;
        public DbSet<VirtualShop.Models.ProductImage> ProductImages { get; set; } = default!;

        public AppUserViewModel GetLoggedInUser()
        {
            //if (LoggedInUser != null) return LoggedInUser;

            var userId = _contextAccessor.HttpContext.Session.GetString(GlobalConfig.LoginSessionName);
            if (!string.IsNullOrEmpty(userId))
            {
                var user = AppUsers.Where(m => m.Id == userId)
                .Select(m => new AppUserViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    DbEntryTime = m.DbEntryTime,
                    Email = m.Email,
                    Roles = m.Roles.Select(n => n.Name).ToList()
                }).FirstOrDefault();
                LoggedInUser = user;
                return LoggedInUser;
            }
            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasMany(x=> x.Products).WithOne(x=> x.Category).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<VirtualShop.Models.ProductViewModel> ProductViewModel { get; set; }

        public DbSet<VirtualShop.Models.AppRole> AppRole { get; set; }
    }
}
