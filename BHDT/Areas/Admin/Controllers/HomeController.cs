using BHDT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class HomeController : Controller
    {
        BHDTEntities1 db = new BHDTEntities1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();// lấy só lượng người truy câpp
            ViewBag.SoLuongNguoiOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();// lấy só lượng người truy câpp
            ViewBag.Tongdoanhthu = ThongKeDoanhThu();
            ViewBag.ddh = ThongKeDonHang();
            ViewBag.kh = KhachHang();

            //ViewBag.TongdoanhthuThang = ThongKeDoanhThuThang(int Thang,int Nam) ;
            return View();
        }
       
        public int KhachHang()
        {

            int kh = db.TaiKhoan.Count();

            return kh;
        }
        public double ThongKeDonHang()
        {

            double dh = db.DonHang.Count();

            return dh;
        }
        public string ThongKeDoanhThu()
        {
            var lstCDH = db.CT_DonHang.Where(n => n.DonHang.MaLoaiTT==2);
            double Tongdoanhthu =0;
            
            foreach (var item in lstCDH)
            {
                Tongdoanhthu = (double)db.CT_DonHang.Sum(n => n.SoLuong * n.DonGia);
            }
      
            
            return Tongdoanhthu.ToString("###,#đ");
        }
        public decimal ThongKeDoanhThuThang(int Thang,int Nam)
        {
            var lstDH = db.DonHang.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
                decimal TongTien = 0;
           
                foreach (var item in lstDH)
                {
                    TongTien += decimal.Parse(item.CT_DonHang.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
                }
                return TongTien;
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult Login(string username, string password)
        //{
        //    Admin_user user = db.Admin_user.SingleOrDefault(x => x.UserName == username && x.PassWord == password && x.Allowed == 1);
        //    if (user != null)
        //    {
        //        Session["userid"] = user.UserId;
        //        Session["username"] = user.UserName;

        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu ";
        //    return View();
        //}
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}