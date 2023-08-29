using Microsoft.AspNetCore.Mvc;
using VirtualShop.Data;
using VirtualShop.Models;

namespace VirtualShop.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            var user = _context.AppUsers.Where(m=>m.Email== model.Email).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email or password is incorrect.");
            }
            if ((user.Id+model.Password).Encrypt()==user.EncryptedPassword)
            {
                HttpContext.Session.SetString(GlobalConfig.LoginSessionName,user.Id);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(GlobalConfig.LoginSessionName);
            return RedirectToAction("Index", "Home");
        }
    }
}
