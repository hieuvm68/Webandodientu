using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;

namespace BHDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "QuanTri,QLKhachHang")]
    public class KhachHangController : Controller
    {
        BHDTEntities1 db = new BHDTEntities1();

        // GET: Admin/KhachHang
        public ActionResult Index()
        {
           return View( db.TaiKhoan.ToList());
        }

        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan khachHang = db.TaiKhoan.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        //// GET: Admin/KhachHang/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "Username");
        //    return View();
        //}

        //// POST: Admin/KhachHang/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaKH,TenKH,DiaChi,Email,SDT,MaKH")] KhachHang khachHang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.KhachHangs.Add(khachHang);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "Username", khachHang.MaKH);
        //    return View(khachHang);
        //}

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan khachHang = db.TaiKhoan.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "Username", khachHang.MaTK);
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "Username", khachHang.MaTK);
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan khachHang = db.TaiKhoan.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan khachHang = db.TaiKhoan.Find(id);
            db.TaiKhoan.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
