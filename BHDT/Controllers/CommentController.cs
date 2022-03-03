using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDT.Models;
namespace BHDT.Controllers
{
    public class CommentController : Controller
    {
        //DbContext
        public BHDTEntities1 database = new BHDTEntities1();

        //// GET: Comments
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult GetUsers()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult GetUsers(string username)
        //{
        //    KhachHang user = database.KhachHangs.Where(u => u.TenKH.ToLower() == username.ToLower())
        //                         .FirstOrDefault();

        //    if (user != null)
        //    {
        //        Session["UserID"] = user.MaKH;
        //        return RedirectToAction("PostComment");
        //    }

        //    ViewBag.Msg = "Username does not exist !";
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult PostComment()
        //{
        //    IQueryable<SanPham> sp = database.SanPhams
        //                                         .Select(p => new SanPham
        //                                         {
        //                                             MaSP = p.MaSP,
        //                                             TenSP = p.TenSP
        //                                         }).AsQueryable();

        //    return View(sp);
        //}

        //public PartialViewResult GetComments(int IdSP)
        //{
        //    IQueryable<Comment> comments = database.Comments.Where(c => c.SanPham.MaSP == IdSP)
        //                                               .Select(c => new Comment
        //                                               {
        //                                                   ComID = c.ComID,
        //                                                   CommentedDate = c.CommentedDate.Value,
        //                                                   CommentMsg = c.CommentMsg,
        //                                                   user = new KhachHang
        //                                                   {
        //                                                       MaKH = c.user.MaKH,
        //                                                       TenKH = c.user.TenKH,
        //                                                       imageProfile = c.user.imageProfile
        //                                                   }
        //                                               }).AsQueryable();

        //    return PartialView("~/Views/Shared/_MyComments.cshtml", comments);
        //}

        //[HttpPost]
        //public ActionResult AddComment(Comment comment, int IdSP)
        //{
        //    //bool result = false;
        //    Comment commentEntity = null;
        //    int userId = (int)Session["UserID"];

        //    var user = database.KhachHangs.FirstOrDefault(u => u.MaKH == userId);
        //    var post = database.SanPhams.FirstOrDefault(p => p.MaSP == IdSP);

        //    if (comment != null)
        //    {

        //        commentEntity = new Comment
        //        {
        //            CommentMsg = comment.CommentMsg,
        //            CommentedDate = comment.CommentedDate,
        //        };


        //        if (user != null && post != null)
        //        {
        //            post.Comments.Add(commentEntity);
        //            user.Comments.Add(commentEntity);

        //            database.SaveChanges();
        //            //result = true;
        //        }
        //    }

        //    return RedirectToAction("GetComments", "Comments", new { postId = IdSP });
        //}

        //[HttpGet]
        //public PartialViewResult GetSubComments(int ComID)
        //{
        //    IQueryable<SubComment> subComments = database.SubComments.Where(sc => sc.Comment.ComID == ComID)
        //                                                      .Select(sc => new SubComment
        //                                                      {
        //                                                          SubComID = sc.SubComID,
        //                                                          CommentMsg = sc.CommentMsg,
        //                                                          CommentedDate = sc.CommentedDate.Value,
        //                                                          users = new KhachHang
        //                                                          {
        //                                                              MaKH = sc.users.MaKH,
        //                                                              TenKH = sc.users.TenKH,
        //                                                              imageProfile = sc.users.imageProfile
        //                                                          }
        //                                                      }).AsQueryable();

        //    return PartialView("~/Views/Shared/_MySubComments.cshtml", subComments);
        //}

        //[HttpPost]
        //public ActionResult AddSubComment(SubComment subComment, int ComID)
        //{
        //    SubComment subCommentEntity = null;
        //    int userId = (int)Session["UserID"];

        //    var user = database.KhachHangs.FirstOrDefault(u => u.MaKH == userId);
        //    var comment = database.Comments.FirstOrDefault(p => p.ComID == ComID);

        //    if (subComment != null)
        //    {

        //        subCommentEntity = new SubComment
        //        {
        //            CommentMsg = subComment.CommentMsg,
        //            CommentedDate = subComment.CommentedDate,
        //        };


        //        if (user != null && comment != null)
        //        {
        //            comment.SubComments.Add(subCommentEntity);
        //            user.SubComments.Add(subCommentEntity);

        //            database.SaveChanges();
        //            //result = true;
        //        }
        //    }

        //    return RedirectToAction("GetSubComments", "Comments", new { ComID = ComID });

        //}
    }
}