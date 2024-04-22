using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PJC.Models;

namespace PJC.Controllers
{
    public class ThongKeController : Controller
    {
        public IActionResult Index()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View();
        }
        public IActionResult Index1()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View(context.GetSanPham());
        }
        public IActionResult Index2()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View(context.GetDocGia());
        }
        public IActionResult Index3()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View(context.GetPhieuMuon());
        }
        public IActionResult Index4()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View(context.GetPhieuTra());
        }
        public IActionResult Index5()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View(context.GetPhieuChuaTra());
        }
        public IActionResult Index6()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(PJC.Models.StoreContext)) as StoreContext;
            int a = context.DemSach();
            int b = context.DemDocGia();
            int c = context.DemPhieuMuon();
            int d = context.DemPhieuTra();
            int e = context.DemPhieuChuaTra();
            double f = context.DemDoanhThu();
            ViewBag.SoLuongSach = a;
            ViewBag.SoLuongDocGia = b;
            ViewBag.SoLuongPhieuMuon = c;
            ViewBag.SoLuongPhieuTra = d;
            ViewBag.SoLuongPhieuChuaTra = e;
            ViewBag.DoanhThu = f;
            return View(context.GetPhieuTraBiPhat());
        }
    }
}
