using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BHDT.Models;
namespace demo.DAO
{
    public class ProductDAO
    {
        BHDTEntities1 db = null;
        public ProductDAO()
        {
            db = new BHDTEntities1();
        }
        //public List<SanPham> ListAll()
        //{
        //    return db.SanPhams.Where(x => x.TinhTrang == true).ToList();
        //}
        public SanPham ViewDetail(int id)
        {
            return db.SanPham.Find(id);
        }
    }
}