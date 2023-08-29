using Microsoft.AspNetCore.Mvc;
using VirtualShop.Data;

namespace VirtualShop.Controllers
{
    public class SQLController : Controller
    {
        private AppDbContext _context;

        public SQLController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Distinct()
        {
            //SQL Query
            //select distinct name from categories

            //Link
            _context.Categories.Select(m=>m.Name).Distinct().ToList();
            return View();
        }

        public IActionResult Where()
        {
            //SQL Query
            //select * from categories where name = 'Mobiles'

            //Link
            _context.Categories.Where(m => m.Name== "Mobile").ToList();
            return View();

            //SQL Query
            //select * from categories where name = 'Mobiles' AND status = 1

            //Link
            _context.Categories.Where(m => m.Name == "Mobile").ToList();
            return View();
        }

        public IActionResult AndOr()
        {
            //SQL Query
            //select * from categories where name = 'Mobiles' OR name = 'Laptop'

            //Link
            _context.Categories.Where(m => m.Name == "Mobile" || m.Name == "Laptop").ToList();
            return View();

            //SQL Query
            //select * from categories where name = 'Mobiles' AND name = 'Laptop'

            //Link
            _context.Categories.Where(m => m.Name == "Mobile" && m.Name == "Laptop").ToList();
            return View();
        }

        public IActionResult OrderBy()
        {
            //SQL Query
            //select * from categories order by type

            //Link
            _context.Categories.OrderBy(m => m.Type).ToList();
            return View();
        }
    }
}
