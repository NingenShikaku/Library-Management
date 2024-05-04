using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJC.Models;

namespace PJC.Areas.User.Controllers
{
    //[Authorize]

    [Area("User")]
    [Route("[area]/[controller]")]
    [Route("[area]/[controller]/[action]")]
    public class PhieuTraController : Controller
    {

        public IActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            return View(context.GetPhieuTra());
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            ViewBag.mapm = HttpContext.Session.GetString("mapm");
            return View();
        }
        [HttpPost]
        public IActionResult Create(PhieuTra pt)
        {
            int count;

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            ViewBag.mapm = HttpContext.Session.GetString("mapm");
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.CreatePhieuTra(pt);
            if (count > 0)
            {
                TempData["result"] = "Thêm phiếu trả thành công";
            }
            else
            {
                TempData["result"] = "Thêm phiếu trả không thành công";
            }
            return Redirect("~/User/PhieuTra/Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            PhieuTra pt = context.getPTByPM(id);
            ViewData.Model = pt;
            return View();
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            bool success = context.DeletePhieuTra(id);
            string redirect_uri = string.Empty;
            if (success)
            {
                return Json(new
                {
                    success,
                    msg = "Xóa thành công",
                    redirect_uri = Url.Action("Index","PhieuTra", new {area = "User"})
                });
            }
            return Json(new
            {
                success,
                msg = "Có lỗi xảy ra khi xóa, vui lòng thử lại sau",
            });
        }
       
      
        [HttpGet]
        public IActionResult Detail(string id, string masach, string madocgia)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            PhieuTra pt = context.getPTByPM(id);
            ViewData.Model = pt;
            return View();
        }
        [HttpGet]

        public IActionResult CTPM(string id)
        {
            ViewBag.mapm = id;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            return View(context.GetPhieuTraByMaPM(id));
        }
    }
}
