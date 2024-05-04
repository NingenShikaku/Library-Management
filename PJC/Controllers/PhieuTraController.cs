using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PJC.Models;

namespace PJC.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [Route("[controller]/[action]")]
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
            return Redirect("/PhieuTra/Index");
        }
        [HttpGet]
        public IActionResult Edit(string id,string masach, string madocgia)
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
                    redirect_uri = Url.Action("Index")
                });
            }
            return Json(new
            {
                success,
                msg = "Có lỗi xảy ra khi xóa, vui lòng thử lại sau",
            });
        }
        /* [HttpPost]*/
        /*public IActionResult Edit(PhieuMuonInCTPM pt)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.UpdatePhieuTra(pt);
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
        }*/
        /* [HttpGet]
         public IActionResult Delete(string id,string masach, string madocgia)
         {
             StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
             PhieuTra pt = context.GetPhieuTraByMaPM(id,masach, madocgia);
             ViewData.Model = pt;
             return View();
         }*/
        /*[HttpPost]
        public IActionResult DeletePhieuTra(PhieuTra pt)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.DeletePhieuTra(pt);
            if (count > 0)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["result"] = "Xóa không thành công";
                return RedirectToAction(nameof(Index));
            }
        }*/
        [HttpGet]
        public IActionResult Detail(string id,string masach, string madocgia)
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
