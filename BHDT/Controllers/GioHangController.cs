using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;

namespace BHDT.Controllers
{
    
    public class GioHangController : Controller
    {
        BHDTEntities1 db = new BHDTEntities1();
        //Lấy giỏ hàng
        public List<itemGioHang> LayGioHang()
        {
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null)
            {
                //nếu sessection giỏ hang chưa tồn tại tạo ra list giỏ
               lstGioHang = new List<itemGioHang>();
                Session["GioHang"] = lstGioHang;
               
            }
                return lstGioHang;
        }
//Thêm Giỏ Hàng
public ActionResult ThemGioHang(int MaSP,string strURL)
        {
            //Kiểm tra sản phẩm tồn tại trong csdl
            SanPham sp =db.SanPham.SingleOrDefault(n=>n.MaSP==MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng
            List < itemGioHang > lstGioHang = LayGioHang();
            //TH1 nếu sp tồn tại trong giỏ hàng

            itemGioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spcheck != null)
            {
                if (sp.SoLuong < spcheck.SoLuong) { return View("ThongBao"); }
                spcheck.SoLuong++;
                spcheck.ThanhTien = spcheck.SoLuong * spcheck.DonGia;
                return Redirect(strURL);
            }
          

            itemGioHang itemGH = new itemGioHang(MaSP);
            if (sp.SoLuong < itemGH.SoLuong) { return View("ThongBao"); }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }
        //Tính Tổng Tiền
        public decimal TinhTongTien()
        {
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }

        //Tổng số lượng 
        public double TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);

        }
        // GET: GioHang
        public ActionResult XemGioHang()
        {//Lấy giỏ hàng
            List<itemGioHang> lstGioHang = LayGioHang();
                 ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TinhTongTien = TinhTongTien().ToString("###,#đ");
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            List<itemGioHang> lstGioHang = LayGioHang();
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TinhTongTien = 0;
                return PartialView(lstGioHang);
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TinhTongTien = TinhTongTien().ToString("###,#đ");

            return PartialView(lstGioHang);
        }
        //Chỉnh Sửa Giỏ Hàng
        [HttpGet]
        public ActionResult SuaGioHang (int MaSP)
        {
            //kiểm tra sesstion tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ acction
            List<itemGioHang> lstGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm đó có tồn tại trong giỏ hang hay ko
            itemGioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
           
            if (spcheck == null) { return RedirectToAction("Index", "Home"); }
            ViewBag.GioHang = lstGioHang;
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TinhTongTien = TinhTongTien().ToString("###,#đ");
            return View(spcheck);
        }
        //Cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(itemGioHang itemGH)
        {
            //Kiểm tra số lượng tồn
            SanPham spCheck = db.SanPham.Single(n => n.MaSP == itemGH.MaSP);

            if (spCheck.SoLuong < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            List<itemGioHang> lstGH = LayGioHang();
            itemGioHang itemGHupdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            itemGHupdate.SoLuong = itemGH.SoLuong;
            itemGHupdate.ThanhTien = itemGHupdate.SoLuong*itemGHupdate.DonGia;
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XoaGioHang(int MaSP)
        {
            //kiểm tra sesstion tồn tại chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = db.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ acction
            List<itemGioHang> lstGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm đó có tồn tại trong giỏ hang hay ko
            itemGioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);

            if (spcheck == null) { return View("ThongBao"); }
            lstGioHang.Remove(spcheck);
           
            return RedirectToAction("XemGioHang");
        }
        public ActionResult MuaNgay(TaiKhoan kh)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TaiKhoan khang = new TaiKhoan();
            //if (Session["email"] == null)
            //{

            //    khang = kh;
            //    db.KhachHangs.Add(khang);
            //    db.SaveChanges();
            //}
            //if (Session["email"] != null)
            //{
            //    KhachHang khach = Session["email"] as KhachHang;
            //    khang.Email = khach.Email;
            //    khang.DiaChi = khach.DiaChi;
            //    khang.SDT = khach.SDT;
            //    khang.TenKH = khach.TenKH;
            //    //db.KhachHangs.Add(khang);
            //    //db.SaveChanges();
            //}
            //Thêm đơn hàng
                TaiKhoan khach = Session["email"] as TaiKhoan;

            DonHang dh = new DonHang();
            ViewBag.TenKH = new SelectList(db.TaiKhoan, "MaTK", "TenTK", khang.TenTK);
            dh.MaTK = int.Parse(khach.MaTK.ToString());
            dh.NgayDat = DateTime.Now;
            dh.MaLoaiTT = 1;
            //dh.DaThanhToan = false;
            //dh.DaHuy = false;
            db.DonHang.Add(dh);
            db.SaveChanges();
            //thêm chi tiết đơn hàng
            List<itemGioHang> lstGH = LayGioHang();
            foreach(var item in lstGH)
            {
                CT_DonHang ctdh = new CT_DonHang();
                ctdh.MaDDH = dh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.DonGia;
                
                db.CT_DonHang.Add(ctdh);
                db.SaveChanges();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TinhTongTien = TinhTongTien().ToString("###,#đ");
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
           
        }
    }
}