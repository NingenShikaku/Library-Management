using Microsoft.AspNetCore.Mvc;
using PJC.Models;

namespace PJC.Areas.User
{
    [Area("User")]
    [Route("[area]/[controller]")]
    [Route("[area]/[controller]/[action]")]
    public class ProductController : Controller
    {
        private StoreContext context;
        void setDBContext()
        {
            if (context == null)
                context = HttpContext.RequestServices.GetService(typeof(StoreContext)) as StoreContext;
        }
        public IActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            return View(context.GetSanPham());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sach sach)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.CreateSach(sach);
            if (count > 0)
            {
                TempData["result"] = "Thêm mới sách thành công";
            }
            else
            {
                TempData["result"] = "Thêm mới sách không thành công";
            }
            return Redirect("~/User/Product/Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            Sach s = context.GetSachByMa(id);
            ViewData.Model = s;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Sach s)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.UpdateProduct(s);
            if (count > 0)
            {
                TempData["result"] = "Cập nhật thành công";
                return Redirect("~/User/Product/Index");
            }
            else
            {
                TempData["result"] = "Cập nhật không thành công";
                return Redirect("~/User/Product/Index");
            }
        }
        //[HttpGet]
        //public IActionResult Delete(string id)
        //{
        //    StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
        //    Sach s = context.GetSachByMa(id);
        //    ViewData.Model = s;
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult Delete(Sach s)
        //{
        //    int count;
        //    StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
        //    count = context.DeleteSach(s);
        //    if (count > 0)
        //    {
        //        TempData["result"] = "Xóa sách  thành công";
        //        return Redirect("~/User/Product/Index");
        //    }
        //    else
        //    {
        //        TempData["result"] = "Xóa sách không thành công";
        //        return Redirect("~/User/Product/Index");
        //    }
        //}

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            bool success = context.DeleteSach(id);
            string redirect_uri = string.Empty;
            if (success)
            {
                return Json(new
                {
                    success,
                    msg = "Xóa thành công",
                    redirect_uri = Url.Action("Index","Product",new {area="User"})
                });
            }
            return Json(new
            {
                success,
                msg = "Có lỗi xảy ra khi xóa, vui lòng thử lại sau",
            });
        }
        [HttpGet]
        public IActionResult Detail(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            Sach s = context.GetSachByMa(id);
            ViewData.Model = s;
            return View();
        }
    }
}
