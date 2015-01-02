namespace OplachiSe.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Web;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OplachiSe.Data.Contracts;
    using OplachiSe.Web.Models.CommentModels;
    using OplachiSe.Models;

    public class CommentController : BaseController
    {
        public CommentController(IOplachiSeData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddComment(AddCommentViewModel comment)
        {
            if (comment != null && ModelState.IsValid)
            {
                var newComment = Mapper.Map<Comment>(comment);
                newComment.Author = this.UserProfile;
                newComment.CreatedOn = DateTime.Now;
                this.Data.Comments.Add(newComment);
                this.Data.SaveChanges();
                return RedirectToAction("ComplainComments", new {id = comment.ComplainId});
            }

            throw new HttpException(400, "Invalid data");
        }

        [HttpPost]

        public ActionResult Like(int value, int commentId)
        {
            if (!Request.IsAjaxRequest())
            {
                throw new HttpException(400, "Bad request");
            }
            var comment = this.Data
                .Comments
                .All()
                .Where(c => c.Id == commentId)
                .FirstOrDefault();

            if (comment == null)
            {
                throw new HttpException(400, "Invalid comment");
            }
             var likedByUser = comment
                 .Likes
                 .Where(c => c.ByUserId == this.UserProfile.Id)
                 .FirstOrDefault();
            if (likedByUser == null)
            {
                comment.Likes.Add(new Like() { Value = value, ByUserId = this.UserProfile.Id });
            }
            else
            {
                likedByUser.Value = value;
            }

            var likes = comment.Likes.Sum(l => l.Value);
            this.Data.SaveChanges();

            return Content(likes.ToString());
        }

        public ActionResult ComplainComments(int id)
        {
            var comments = this.Data
                .Comments
                .All()
                .Where(c => c.ComplainId == id)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ShowCommentViewModel>()
                .ToList();


            return PartialView("_DisplayCommentPartial", comments);
        }
    }
}