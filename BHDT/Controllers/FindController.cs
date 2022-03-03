using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;
namespace BHDT.Controllers
{
    public class FindController : Controller
    {
        BHDTEntities1 db = new BHDTEntities1();
        // GET: Find
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            var lstSP = db.SanPham.Where(n => n.TenSP.Contains(sTuKhoa));
                return View(lstSP.OrderBy(n=>n.TenSP));
        }
    }
}