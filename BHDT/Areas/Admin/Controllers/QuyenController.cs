using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;
namespace BHDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "QuanTri")]
    //[Authorize(Roles = "QLQuyen")]
    public class QuyenController : Controller
    {
        BHDTEntities1 db = new BHDTEntities1();
        // GET: Admin/Quyen
        public ActionResult Index()
        {
            return View(db.Quyen.OrderBy(n=>n.MaQuyen));
        }
        [HttpGet]
        public ActionResult ThemQuyen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemQuyen(Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                db.Quyen.Add(quyen);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
             
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}