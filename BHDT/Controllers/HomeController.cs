using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;
using PagedList;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity;
using System.Web.Security;

namespace BHDT.Controllers
{
   
    public class HomeController : Controller
    {
      
        BHDTEntities1 db = new BHDTEntities1();

        public ActionResult MenuPartial()
        {
            //truyvan lay ve 1 list cac san pham
            var lstSP = db.SanPham;
            return PartialView(lstSP);
        }


        public ActionResult Index(/*int? page*/)
        {//loadsanpham theo 2 tieu chi la loai sp or thuong hieu

            var lstSP = db.SanPham;
           
            //taophantrangsp
            //int PageSize = 8;
            ////so trang hien tai
            //int PageNumber = (page ?? 1);

            return View(lstSP/*.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize)*/);
        }

        public ActionResult ListProductPartial(int? MaLoaiSP, int? MaTH, int? page)
        {//loadsanpham theo 2 tieu chi la loai sp or thuong hieu

            //var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaTH);
            var lstSP = db.SanPham.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaTH);
            //taophantrangsp
            int PageSize = 10;
            //so trang hien tai
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaTH = MaTH;
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult ListProductPartialLSP(int? MaLoaiSP, int? MaTH, int? page)
        {//loadsanpham theo 2 tieu chi la loai sp or thuong hieu

            //var lstSP = db.SanPhams.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaTH);
            var lstSP = db.SanPham.Where(n => n.MaLoaiSP == MaLoaiSP || n.MaNSX == MaTH);

            //taophantrangsp
            int PageSize = 10;
            //so trang hien tai
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaTH = MaTH;
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult Dangky()

        { return RedirectToAction("Index"); }


        //POST: Register
        [HttpPost]
        public ActionResult Dangky(TaiKhoan kh, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                kh.MaLoaiTK = 1;
                string semail = f["Email"].ToString();
                string smatkhau = f["Password"].ToString();
                
                var check = db.TaiKhoan.FirstOrDefault(s => s.Email == semail);
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                   
                    db.TaiKhoan.Add(kh);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                else RedirectToAction("Index");


            }
            return RedirectToAction("Index");


        }




        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //string susername = f["email"].ToString();
            //string smatkhau = f["password"].ToString();
            //KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.Email == susername && n.Password == smatkhau);
            //if (kh != null)
            //{
            //    Session["email"] = kh;
            //    //return RedirectToAction("Index");
            //    return Content("<script>window.location.reload()</script>");
            //}
            //return Content("Tài khoản hoặc mật khẩu của bạn không đúng");
            string susername = f["email"].ToString();
            string smatkhau = f["password"].ToString();
            TaiKhoan kh = db.TaiKhoan.SingleOrDefault(n => n.Email == susername && n.Password == smatkhau);
            if (kh != null)
            {
                var lstQuyen = db.LoaiQuyenTK.Where(n => n.MaLoaiTK == kh.MaLoaiTK);
                string Quyen = "";
                foreach (var item in lstQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(kh.Email.ToString(), Quyen);
                Session["email"] = kh;
                return Content("<script>window.location.reload()</script>");
            }
            return Content("Tài khoản hoặc mật khẩu của bạn không đúng");
        }
        public void PhanQuyen(string Email,string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, Email, DateTime.Now, DateTime.Now.AddHours(3),
                false
                , Quyen, FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        public ActionResult DangXuat()
        {
            Session["email"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        //public ActionResult Prfile(int? idkh)
        //{
        //    KhachHang kh = db.KhachHangs.Where(n => n.MaKH == idkh).SingleOrDefault();

        //    return View();
        //}
        [HttpGet]
        public ActionResult Prfile(int? idkh)
        {
            ViewBag.MaTK = new SelectList(db.TaiKhoan.OrderBy(n => n.Email), "MaTK", "TenTK");

            if (idkh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan model = db.TaiKhoan.SingleOrDefault(n => n.MaTK == idkh);
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var kh = db.TaiKhoan.Where(n => n.MaTK == idkh);
            ViewBag.Profile = kh;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Prfile(FormCollection col)
        {
            
            string name = col["txtTen"];
            string diachi = col["txtdiachi"];
            string sdt = col["txtsdt"];
            string bietdanh = col["txtbietdanh"];
            string email = col["txtemail"];

            var file = Request.Files["UploadImage"];
            var ext = Path.GetExtension(file.FileName).ToLower(); //lấy đuôi của file
            var FileName = Path.GetFileName(file.FileName); // lấy tên file
            //var FileNameNull = FileName.Remove(FileName.LastIndexOf(".")); //lấy mỗi tên file không chưa đuôi file.
            if (ext == "" || (ext != ".jpg" && ext != ".png" && ext != ".jpeg" && ext != ".gif"))
            {
                ModelState.AddModelError("", "File phải có đuôi jpg, png, gif");
            }
            else if (file.ContentLength > 0 && file.ContentLength < 10048576 && FileName != "")
            {
                var path = Path.Combine(Server.MapPath("~/Images"), FileName);
                file.SaveAs(path);
                ModelState.AddModelError("", "Lưu file thành công");
            }
            else
            {
                ModelState.AddModelError("", "File không tồn tại hoặc file phải nhỏ hơn 2MB");
            }
          

            TaiKhoan k = (TaiKhoan)Session["email"];
            k.ImageUser = file.FileName;
            TaiKhoan khang = db.TaiKhoan.SingleOrDefault(n => n.MaTK ==k.MaTK);
            khang.TenTK = name;
            khang.DiaChi = diachi;
            khang.SDT = sdt;
            khang.Username = bietdanh;
            khang.ImageUser = file.FileName;
            db.SaveChanges();
            
            return View(khang);
        }

        public ActionResult DonDuyet()
        {
            TaiKhoan khach = Session["email"] as TaiKhoan;

            var lstdonduyet = db.DonHang.Where(n => n.MaLoaiTT == 2 && n.MaTK == khach.MaTK).OrderBy(n => n.NgayGiao);
            //ViewBag.ListCTDH = lstdonduyetchua;
            return View(lstdonduyet);
        }
        public ActionResult DonChuaDuyet()
        {
            //KhachHang kh = db.KhachHangs.Where(n => n.MaKH == idkh).SingleOrDefault();
            TaiKhoan khach = Session["email"] as TaiKhoan;
            var lstdonduyetchua = db.DonHang.Where(n => n.MaLoaiTT == 1 && n.MaTK ==khach.MaTK).ToList();
            //ViewBag.ListCTDH = lstdonduyetchua;
            return View(lstdonduyetchua);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang kh = db.DonHang.Find(id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        // POST: Admin/NhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang dh = db.DonHang.Find(id);
            db.DonHang.Remove(dh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
   

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult CTSP(int? idsp)
        {
            SanPham sp = db.SanPham.Where(n => n.MaSP == idsp).SingleOrDefault();
           

            return View(sp);
        }

    }
}