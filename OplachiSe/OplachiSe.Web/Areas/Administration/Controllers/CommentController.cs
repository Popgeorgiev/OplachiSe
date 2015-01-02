namespace OplachiSe.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OplachiSe.Data;
    using OplachiSe.Models;
    using OplachiSe.Web.Areas.Administration.Controllers;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using OplachiSe.Data.Contracts;

    using Model = OplachiSe.Models.Comment;
    using ViewModel = OplachiSe.Web.Areas.Administration.Models.CommentViewModel;

    public class CommentController : KendoGridAdministrationController
    {

        public CommentController(IOplachiSeData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data
                .Comments
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Comments.Find(id) as T;
        }


        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var comment = this.Data.Comments.Find(model.Id.Value);

                var likes = this.Data
                        .Likes
                        .All()
                        .Where(l => l.CommentId == comment.Id)
                        .Select(l => l.Id)
                        .ToList();

                foreach (var likeId in likes)
                {
                    this.Data.Likes.Delete(likeId);
                }
                this.Data.SaveChanges();

                this.Data.Comments.Delete(comment);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

    }
}