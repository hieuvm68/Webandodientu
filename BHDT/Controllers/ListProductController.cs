using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;
namespace BHDT.Controllers
{
    public class ListProductController : Controller
    {
        // GET: ListProduct
        BHDTEntities1 db = new BHDTEntities1();
        // GET: SanPham

        public ActionResult ListProductPartial()
        {
            var lstSP = db.SanPham;
            //Gan vao viewbag
            ViewBag.ListSP = lstSP;

            return View(lstSP);
        }

       
        public ActionResult ListProductPartial(int? MaLoaiSP, int? MaTH)
        {//loadsanpham theo 2 tieu chi la loai sp or thuong hieu
            if (MaLoaiSP == null && MaTH == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPham.Where(n => n.MaLoaiSP == MaLoaiSP || n.MaNSX == MaTH);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(lstSP);
        }
    }
}