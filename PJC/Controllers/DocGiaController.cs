using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJC.Models;

namespace PJC.Controllers
{
    // [Authorize]
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class DocGiaController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            return View(context.GetDocGia());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DocGia dg)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.CreateDocGia(dg);
            if (count > 0)
            {
                TempData["result"] = "Thêm mới độc giả thành công";
            }
            else
            {
                TempData["result"] = "Thêm mới độc giả không thành công";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            DocGia dg = context.GetDocGiaByMaDG(id);
            ViewData.Model = dg;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(DocGia dg)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.UpdateDocGia(dg);
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
        //    DocGia dg = context.GetDocGiaByMaDG(id);
        //    ViewData.Model = dg;
        //    return View();
        //}




        //[HttpPost]
        //public IActionResult Delete(DocGia dg)
        //{
        //    int count;
        //    StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
        //    count = context.DeleteDocGia(dg);
        //    if (count > 0)
        //    {
        //        TempData["result"] = "Xóa độc giả  thành công";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        TempData["result"] = "Xóa độc giả không thành công";
        //        return RedirectToAction(nameof(Index));
        //    }
        //}


        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            bool success = context.DeleteDocGia(id);
            string redirect_uri = string.Empty;
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

        public IActionResult Detail(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            DocGia s = context.GetDocGiaByMaDG(id);
            ViewBag.madg = id;

            ViewData.Model = s;
            return View();
        }
    }
}
