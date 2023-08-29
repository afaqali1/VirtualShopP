using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualShop;
using VirtualShop.Data;
using VirtualShop.Handlers;
using VirtualShop.Models;

namespace TaskJuly24.Controllers
{
    //[Authorized(Roles = $"{GlobalConfig.AdminRole}, {GlobalConfig.ShopkeeperRole}")]
    [Authorized]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext model)
        {
            _context = model;
        }
        public IActionResult Index()
        {
            var products = _context.Products
                .Select(m => new ProductViewModel
                {
                    Brand = m.Brand.Name,
                    Name = m.Name,
                    Category = m.Category.Name,
                    Description = m.Description,
                    Price = m.Price,
                    Slug = m.Slug,
                    Id = m.Id,
                    Stock = m.Stock,
                    ImageUrl = m.Images.OrderBy(n => n.DbEntryTime).Select(n => n.Url).FirstOrDefault()
                }).ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            if (!_context.Categories.Any(m => m.Status && m.Type == CategoryType.Category))
            {
                TempData["notification"] = "No category exists with the active status";
                return RedirectToAction("Create", "Categories");
            }

            if (!_context.Categories.Any(m => m.Status && m.Type == CategoryType.Brand))
            {
                TempData["notification"] = "No brand exists with the active status";
                return RedirectToAction("Create", "Categories");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                if (model.Uploads?.Any() == true)
                {
                    
                    model.Images ??= new();
                    int imageRank = 0;
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "products");
                    string directryPath = Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);

                    foreach (var item in model.Uploads)
                    {
                        if (item.Length > 0)
                        {
                            string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(item.FileName);

                            using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);
                            item.CopyTo(stream);
                            model.Images.Add(new ProductImage
                            {
                                Rank = ++imageRank,
                                ProductId = model.Id,
                                Url = Path.Combine(appPath, fileName).Replace("\\", "/")
                            });
                        }
                    }

                    _context.Add(model);
                    int r = _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(string id)
        {
            //Products/Edit/skldfjsldj
            var product = _context.Products
                .Include(m => m.Images)
                .Include(m => m.Category)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            //TempData["notification"] = "No brand exists with the active status";
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model, List<string> deletedImagesIds)
        {
            if (ModelState.IsValid)
            {
                if (model.Uploads?.Any() == true)
                {
                    List<ProductImage> images = new(); // new List<ProductImage>();
                    int imageRank = 0;
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "products");
                    string directryPath = Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);

                    foreach (var item in model.Uploads)
                    {
                        if (item.Length > 0)
                        {
                            string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(item.FileName);

                            using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);
                            item.CopyTo(stream);
                            images.Add(new ProductImage
                            {
                                Rank = ++imageRank,
                                ProductId = model.Id,
                                Url = Path.Combine(appPath, fileName).Replace("\\", "/")
                            });
                        }
                    }

                    var imagesToDelete = _context.ProductImages.Where(m => m.ProductId == model.Id && deletedImagesIds.Contains(m.Id)).ToList();

                    _context.AddRange(images);
                    var imageUrlsToDelete = imagesToDelete.Select(m => m.Url).ToArray();
                    _context.RemoveRange(imagesToDelete);
                    _context.Update(model);

                    int r = _context.SaveChanges();

                    if (r > 0)
                    {
                        foreach (var url in imageUrlsToDelete)
                        {
                            try
                            {
                                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url));
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
