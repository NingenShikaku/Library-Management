using Microsoft.AspNetCore.Mvc;
using PJC.Models;

namespace PJC.Controllers
{
    // [Authorize]
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {

        private StoreContext context;


        void setDBContext()
        {
            if (context == null)
                context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
        }
        public IActionResult Index(string Keyword)
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            return View(context.GetTaiKhoan());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(TaiKhoan tk)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.CreateUser(tk);
            if (count > 0)
            {
                TempData["result"] = "Thêm mới người dùng thành công";
            }
            else
            {
                TempData["result"] = "Thêm mới người dùng không thành công";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            TaiKhoan tk = context.GetTaiKhoanByUser(id);
            ViewData.Model = tk;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(TaiKhoan tk)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.UpdateUser(tk);
            if (count > 0)
            {
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["result"] = "Cập nhật không thành công";
                return RedirectToAction("Index");
            }
        }
        //[HttpGet]
        //public IActionResult Delete(string id)
        //{
        //    StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
        //    TaiKhoan tk = context.GetTaiKhoanByUser(id);
        //    ViewData.Model = tk;
        //    return View();
        //}

        [HttpDelete]
        [Route("delete/{username}")]
        public IActionResult Delete(string username)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            bool success = context.DeleteUser(username);
            if (success)
            {
                return Json(new
                {
                    success,
                    msg = "Xóa thành công",
                    redirect_uri = Url.Action("Index")
                });
            }
            return Json(new
            {
                success,
                msg = "Có lỗi xảy ra khi xóa, vui lòng thử lại sau",
            });
        }


        [HttpPost]
        public IActionResult Delete(TaiKhoan tk)
        {

            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.DeleteUser(tk);

            if (count > 0)
            {
                TempData["result"] = "Xóa người dùng thành công";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["result"] = "Xóa người dùng không thành công";
                //return View();
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
