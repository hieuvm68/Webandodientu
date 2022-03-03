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
    public class NhaSanXuatController : Controller
    {
        private BHDTEntities1 db = new BHDTEntities1();

        // GET: Admin/NhaSanXuat
        public ActionResult Index()
        {
            return View(db.NhaSanXuat.ToList());
        }

        // GET: Admin/NhaSanXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuat.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaSanXuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhaSanXuat nhaSanXuat)
        {
            //string filenamelogo = Path.GetFileNameWithoutExtension(nhaSanXuat.ImageLogo.FileName);
            //string extensionl = Path.GetExtension(nhaSanXuat.ImageLogo.FileName);
            //filenamelogo = filenamelogo + DateTime.Now.ToString("yymmfff") + extensionl;
            //nhaSanXuat.Logo = "/image/" + filenamelogo;
            //filenamelogo = Path.Combine(Server.MapPath("/image/"), filenamelogo);
            //nhaSanXuat.ImageLogo.SaveAs(filenamelogo);
            using (BHDTEntities1 db = new BHDTEntities1())
            {
                db.NhaSanXuat.Add(nhaSanXuat);
                db.SaveChanges();
            }
            ModelState.Clear();

            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuat.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNSX,TenNSX,ThongTin,Logo")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuat.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhaSanXuat nhaSanXuat = db.NhaSanXuat.Find(id);
            db.NhaSanXuat.Remove(nhaSanXuat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SearchName(string searchString)
        {
            return View(db.NhaSanXuat.Where(ns => ns.TenNSX.Contains(searchString) || searchString == null).ToList());

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
