using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using PJC.Models;

namespace PJC.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class PhieuMuonController : Controller
    {

        public IActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            return View(context.GetPhieuMuon());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.sessionv= HttpContext.Session.GetString("user");
            return View();
        }
        [HttpPost]
        public IActionResult Create(PhieuMuon pm)
        {
            int count;
            HttpContext.Session.SetString("mapm", pm.MaPM);
           

            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.CreatePhieuMuon(pm);
            if(count == 100)
            {
                TempData["result"] = "Bạn làm mất sách 3 lần. Không được mượn nữa!";
                return Redirect("/PhieuTra/Index");
            }
            if (count > 0)
            {
                TempData["result"] = "Thêm mới phiếu mượn thành công";
            }
            else
            {
                TempData["result"] = "Thêm mới phiếu mượn không thành công";
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            PhieuMuon pm = context.GetPhieuMuonByMaPM(id);
            ViewData.Model = pm;
            return View();
        }
        [HttpPost]
        public IActionResult Edit(PhieuMuon pm)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.UpdatePhieuMuon(pm);
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
        
        [HttpPost]
        public IActionResult EditSoLuong(string id)
        {
            string a= HttpContext.Session.GetString("mapm");
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.UpdateSoLuongSach(a);
            if (count > 0)
            {
                TempData["result"] = "Cập nhật số lượng sách mượn thành công";
                return RedirectToAction("Index","PhieuMuon");
            }
            else
            {
                TempData["result"] = "Cập nhật số lượng sách mượn không thành công";
                return RedirectToAction("Index","PhieuMuon");
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            bool success = context.DeletePhieuMuon(id);
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
        /*[HttpPost]
        public IActionResult Delete(PhieuMuon pm)
        {
            int count;
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            count = context.DeletePhieuMuon(pm);
            if (count > 0)
            {
                TempData["result"] = "Xóa phiếu mượn  thành công";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["result"] = "Xóa phiếu mượn không thành công";
                return RedirectToAction(nameof(Index));
            }
        }*/
        [HttpGet]
        public IActionResult Detail(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            PhieuMuon pm = context.GetPhieuMuonByMaPM(id);
            ViewData.Model = pm;
            return View();
        }
        [HttpGet]
        public IActionResult GetPMDG(string id)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            ViewBag.madg = id;
            return View(context.GetPhieuMuonByMADG(id));
        }
    }
}
