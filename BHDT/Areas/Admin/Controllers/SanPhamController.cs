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
    public class SanPhamController : Controller
    {
        private BHDTEntities1 db = new BHDTEntities1();
        //[Authorize(Roles = "QLSanPham")]
        //// GET: Admin/SanPham
        public ActionResult Index()
        {

            return View(db.SanPham.Where(n=>n.DaXoa==false).OrderByDescending(n=>n.MaSP));

        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham.OrderBy(n=>n.TenLoai), "MaLoaiSP", "TenLoai");
            //ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat.OrderBy(n=>n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
       public ActionResult Create(SanPham sanPham)
        {
            string fileName = Path.GetFileNameWithoutExtension(sanPham.ImageFile.FileName);
            string fileName1 = Path.GetFileNameWithoutExtension(sanPham.ImageFile1.FileName);
            string fileName2 = Path.GetFileNameWithoutExtension(sanPham.ImageFile2.FileName);
            string fileName3 = Path.GetFileNameWithoutExtension(sanPham.ImageFile3.FileName);
            string fileName4 = Path.GetFileNameWithoutExtension(sanPham.ImageFile4.FileName);
            string extension = Path.GetExtension(sanPham.ImageFile.FileName);
            string extension1 = Path.GetExtension(sanPham.ImageFile1.FileName);
            string extension2 = Path.GetExtension(sanPham.ImageFile2.FileName);
            string extension3 = Path.GetExtension(sanPham.ImageFile3.FileName);
            string extension4 = Path.GetExtension(sanPham.ImageFile4.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmfff") + extension;
            fileName1 = fileName1 + DateTime.Now.ToString("yymmfff") + extension1;
            fileName2 = fileName2 + DateTime.Now.ToString("yymmfff") + extension2;
            fileName3 = fileName3 + DateTime.Now.ToString("yymmfff") + extension3;
            fileName4 = fileName4 + DateTime.Now.ToString("yymmfff") + extension4;
            sanPham.HinhAnh = "/Images/" + fileName;
            sanPham.HinhAnh1 = "/Images/" + fileName1;
            sanPham.HinhAnh2 = "/Images/" + fileName2;
            sanPham.HinhAnh3 = "/Images/" + fileName3;
            sanPham.HinhAnh4 = "/Images/" + fileName4;
            fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
            fileName1 = Path.Combine(Server.MapPath("/Images/"), fileName1);
            fileName2 = Path.Combine(Server.MapPath("/Images/"), fileName2);
            fileName3 = Path.Combine(Server.MapPath("/Images/"), fileName3);
            fileName4 = Path.Combine(Server.MapPath("/Images/"), fileName4);
            sanPham.ImageFile.SaveAs(fileName);
            sanPham.ImageFile1.SaveAs(fileName1);
            sanPham.ImageFile2.SaveAs(fileName2);
            sanPham.ImageFile3.SaveAs(fileName3);
            sanPham.ImageFile4.SaveAs(fileName4);
            using (BHDTEntities1 db = new BHDTEntities1())
            {
                db.SanPham.Add(sanPham);
                db.SaveChanges();
            }
            ModelState.Clear();

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);

        }

    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            //ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(SanPham sanPham)
        {
            string fileName = Path.GetFileNameWithoutExtension(sanPham.ImageFile.FileName);
            string fileName1 = Path.GetFileNameWithoutExtension(sanPham.ImageFile1.FileName);
            string fileName2 = Path.GetFileNameWithoutExtension(sanPham.ImageFile2.FileName);
            string fileName3 = Path.GetFileNameWithoutExtension(sanPham.ImageFile3.FileName);
            string fileName4 = Path.GetFileNameWithoutExtension(sanPham.ImageFile4.FileName);
            string extension = Path.GetExtension(sanPham.ImageFile.FileName);
            string extension1 = Path.GetExtension(sanPham.ImageFile1.FileName);
            string extension2 = Path.GetExtension(sanPham.ImageFile2.FileName);
            string extension3 = Path.GetExtension(sanPham.ImageFile3.FileName);
            string extension4 = Path.GetExtension(sanPham.ImageFile4.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmfff") + extension;
            fileName1 = fileName1 + DateTime.Now.ToString("yymmfff") + extension1;
            fileName2 = fileName2 + DateTime.Now.ToString("yymmfff") + extension2;
            fileName3 = fileName3 + DateTime.Now.ToString("yymmfff") + extension3;
            fileName4 = fileName4 + DateTime.Now.ToString("yymmfff") + extension4;
            sanPham.HinhAnh = "/Images/" + fileName;
            sanPham.HinhAnh1 = "/Images/" + fileName1;
            sanPham.HinhAnh2 = "/Images/" + fileName2;
            sanPham.HinhAnh3 = "/Images/" + fileName3;
            sanPham.HinhAnh4 = "/Images/" + fileName4;
            fileName = Path.Combine(Server.MapPath("/Images/"), fileName);
            fileName1 = Path.Combine(Server.MapPath("/Images/"), fileName1);
            fileName2 = Path.Combine(Server.MapPath("/Images/"), fileName2);
            fileName3 = Path.Combine(Server.MapPath("/Images/"), fileName3);
            fileName4 = Path.Combine(Server.MapPath("/Images/"), fileName4);
            sanPham.ImageFile.SaveAs(fileName);
            sanPham.ImageFile1.SaveAs(fileName1);
            sanPham.ImageFile2.SaveAs(fileName2);
            sanPham.ImageFile3.SaveAs(fileName3);
            sanPham.ImageFile4.SaveAs(fileName4);
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPham, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            //ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SearchName(string searchString)
        {
            return View(db.SanPham.Where(s => s.TenSP.Contains(searchString) || searchString == null).ToList());

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
