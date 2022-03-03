using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;
using System.Net.Mail;

namespace BHDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "QuanTri,QLDonHang")]
    public class DonDatHangController : Controller
    {
        private BHDTEntities1 db = new BHDTEntities1();

        // GET: Admin/DonDatHang
        public ActionResult Index()
        {
           

            var donDatHangs = db.DonHang.Include(d => d.TaiKhoan).Where(n=>n.MaLoaiTT==1);
            return View(donDatHangs.ToList());
        }
        public ActionResult Index2()
        {


            var donDatHangs2 = db.DonHang.Include(d => d.TaiKhoan).Where(n => n.MaLoaiTT == 2);
            return View(donDatHangs2.ToList());
        }

        // GET: Admin/DonDatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ct = db.CT_DonHang.Where(n=>n.MaDDH==id).ToList();
            

            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct.ToList());
        }

        // GET: Admin/DonDatHang/Create
       
        // GET: Admin/DonDatHang/Edit/5
      
        // GET: Admin/DonDatHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donDatHang = db.DonHang.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // POST: Admin/DonDatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donDatHang = db.DonHang.Find(id);
            db.DonHang.Remove(donDatHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SearchDonHang(string searchString)
        {
            return View(db.DonHang.Where(dh => dh.TaiKhoan.TenTK.Contains(searchString) || searchString == null).ToList());

        }
        [HttpGet]
       
        public ActionResult DuyetDonHang(int? id)
        {
            ViewBag.MaLoaiTT = new SelectList(db.TinhtrangDH.OrderBy(n => n.TenTT), "MaLoaiTT", "TenTT");

            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang model = db.DonHang.SingleOrDefault(n => n.MaDDH == id);
            if(model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var listChitiet = db.CT_DonHang.Where(n => n.MaDDH == id);
            ViewBag.ListCTDH = listChitiet;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult DuyetDonHang(DonHang dh)
        {
            ViewBag.MaLoaiTT = new SelectList(db.TinhtrangDH.OrderBy(n => n.TenTT), "MaLoaiTT", "TenTT",dh.MaLoaiTT);

            //ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            if (dh == null)
            {
             return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang ddhUpdate = db.DonHang.Single(n => n.MaDDH == dh.MaDDH);
            ddhUpdate.MaLoaiTT = dh.MaLoaiTT;
            //db.Entry(dh).State = EntityState.Modified;
            db.SaveChanges();
            var listChitiet = db.CT_DonHang.Where(n => n.MaDDH == dh.MaDDH);
            ViewBag.ListCTDH = listChitiet;
            //GuiEmail("Xác Nhận Đơn Hàng của Hệ Thống B2", "hieuvm69@gmail.com", "hieuvm68@gmail.com", "vuminhhieu2000", "Đơn Hàng của bạn đã đặt thành công!");
            return View(ddhUpdate);
        }
        //public void GuiEmail(string Title, string ToEmail, string FromEmail, string Password, string content)
        //{
        //    MailMessage mail = new MailMessage();
        //    mail.To.Add(ToEmail);
        //    mail.From = new MailAddress(ToEmail);
        //    mail.Subject = Title;
        //    mail.Body = content;
        //    mail.IsBodyHtml = true;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new System.Net.NetworkCredential(FromEmail, Password);
        //    smtp.EnableSsl = true;
        //    smtp.Send(mail);
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
