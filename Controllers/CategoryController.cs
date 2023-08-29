using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VirtualShop.Data;
using VirtualShop.Models;

namespace VirtualShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string k)
        {
            var getRecord = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(k))
            {
                getRecord=getRecord.Where(m=>(m.Name.Contains(k)||m.Description.Contains(k)));
            }
            ViewBag.searchUrl = "/Category";
            ViewBag.searchByKey = k;
            var obj = getRecord.ToList();
            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.Logo != null && model.Logo.Length > 0)
                {
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "categories");
                    string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.Logo.FileName);
                    string directryPath = Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);

                    using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);
                    model.Logo.CopyTo(stream);
                    model.LogoUrl = Path.Combine(appPath, fileName).Replace("\\", "/");
                }

                _context.Categories.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Update(String id)
        {
            var cat = _context.Categories.Find(id)
;
            if (cat == null) return NotFound();
            return View(cat);
        }
        [HttpPost]
        public IActionResult Update(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.Logo != null && model.Logo.Length > 0)
                {
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string appPath = Path.Combine("images", "categories");
                    string fileName = Path.GetRandomFileName().Replace(".", "") + Path.GetExtension(model.Logo.FileName);
                    string directryPath = Path.Combine(basePath, appPath);
                    Directory.CreateDirectory(directryPath);

                    using var stream = new FileStream(Path.Combine(directryPath, fileName), FileMode.Create);
                    model.Logo.CopyTo(stream);
                    model.LogoUrl = Path.Combine(appPath, fileName).Replace("\\", "/");
                }

                _context.Categories.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Details(String id)
        {
            var cat = _context.Categories.Find(id)
;
            if (cat == null) return NotFound();
            return View(cat);
        }

        public IActionResult Delete(String id)
        {
            var cat = _context.Categories.Find(id)
;
            if (cat == null) return NotFound();
            return View(cat);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(string id)
        {

            var cat = _context.Categories.Find(id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
