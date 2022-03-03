using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;

namespace BHDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "QuanTri,QLSanPham")]
    public class LoaiSanPhamController : Controller
    {
        private BHDTEntities1 db = new BHDTEntities1();

        // GET: Admin/LoaiSanPham
        public ActionResult Index()
        {
            return View(db.LoaiSanPham.ToList());
        }

        // GET: Admin/LoaiSanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPham.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoaiSanPham loaiSanPham)
        {
            //string filenameloai = Path.GetFileNameWithoutExtension(loaiSanPham.ImageLoai.FileName);
            //string extensionloai = Path.GetExtension(loaiSanPham.ImageLoai.FileName);
            //filenameloai = filenameloai + DateTime.Now.ToString("yymmfff") + extensionloai;
            //loaiSanPham.Icon = "/image/" + filenameloai;
            //filenameloai = Path.Combine(Server.MapPath("/image/"), filenameloai);
            //loaiSanPham.ImageLoai.SaveAs(filenameloai);
            //using (BHDTEntities1 db = new BHDTEntities1())
            {
                db.LoaiSanPham.Add(loaiSanPham);
                db.SaveChanges();
            }
            ModelState.Clear();

            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPham.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiSP,TenLoai,Icon,BiDanh")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.LoaiSanPham.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSanPham loaiSanPham = db.LoaiSanPham.Find(id);
            db.LoaiSanPham.Remove(loaiSanPham);
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
