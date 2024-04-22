using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJC.Models;

namespace PJC.Controllers
{


    public class LoginController : Controller
    {
        
        [HttpGet]
        public IActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(string user,string password)
        {
           
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int kq = context.Login(user, password);
            TempData["userlogin"] = user;
            HttpContext.Session.SetString("user", user);          
            
            if (kq == 1)
            {
                return RedirectToAction("Index","Home");
                //return RedirectToAction("Index", "Home");
            }    
            else if(kq == -1)
            {
                TempData["result"] = "Đăng nhập không thành công! Cần nhập đủ thông tin!";
                return RedirectToAction("Index", "Login");
            }
            return Redirect("~/User/Home/Index");
        }
     
    }
}
