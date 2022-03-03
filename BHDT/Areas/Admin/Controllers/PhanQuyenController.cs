using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BHDT.Models;
namespace BHDT.Areas.Admin.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class PhanQuyenController : Controller
    {
        BHDTEntities1 db = new BHDTEntities1();
        // GET: Admin/PhanQuyen
        public ActionResult Index()
        {
            return View(db.LoaiTK.OrderBy(n=>n.MaLoaiTK));
        }
        [HttpGet]
        public ActionResult PhanQuyen(int? id)
        {
            //lấy mã loạikh
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            LoaiTK lkh = db.LoaiTK.SingleOrDefault(n => n.MaLoaiTK == id);
            if (lkh == null)
            {
                return HttpNotFound();
            }
            //Lấy ra ds quyền để load ra checkbox
            ViewBag.MaQuyen = db.Quyen;
            //lấy ds quyền của loai đó
            ViewBag.LoaiTKQuyen = db.LoaiQuyenTK.Where(n=>n.MaLoaiTK==id);
            return View(lkh);
        }
        [HttpPost]
        public ActionResult PhanQuyen(int? id, IEnumerable<LoaiQuyenTK> lstLTKQuyen)
        {
            //Nếu đã tiến hành phân quyền e nhưng muốn phân quyển lại
            //xóa những loại phân quyền đó
            var lstDaPhanQuyen = db.LoaiQuyenTK.Where(n => n.MaLoaiTK == id);
            if (lstDaPhanQuyen.Count()>=0 )
            {
                db.LoaiQuyenTK.RemoveRange(lstDaPhanQuyen);
                db.SaveChanges();
            }
            if (lstLTKQuyen != null) { 
            //Kiểm tra list ds quyền đc check
            foreach (var item in lstLTKQuyen)
            {
                    if (item.MaQuyen !=null)
                    {
                        item.MaLoaiTK = int.Parse(id.ToString());

                    }
                db.LoaiQuyenTK.Add(item);
            }

            db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
       
    }
}