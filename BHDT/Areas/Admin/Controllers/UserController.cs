//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using BHDT.Models;

//namespace BHDT.Areas.Admin.Controllers
//{
//    public class UserController : Controller
//    {
//        private BHDTEntities1 db = new BHDTEntities1();

//        // GET: Admin/User
//        public ActionResult Index()
//        {
//            return View(db.Admin_user.ToList());
//        }

//        // GET: Admin/User/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Admin_user user = db.Admin_user.Find(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            return View(user);
//        }

//        // GET: Admin/User/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Admin/User/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "UserId,UserName,PassWord,Email,Phone,Allowed")] Admin_user user)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Admin_user.Add(user);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(user);
//        }

//        // GET: Admin/User/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Admin_user user = db.Admin_user.Find(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            return View(user);
//        }

//        // POST: Admin/User/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "UserId,UserName,PassWord,Email,Phone,Allowed")] Admin_user user)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(user).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(user);
//        }

//        // GET: Admin/User/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Admin_user user = db.Admin_user.Find(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            return View(user);
//        }

//        // POST: Admin/User/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Admin_user user = db.Admin_user.Find(id);
//            db.Admin_user.Remove(user);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
