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

    using Model = OplachiSe.Models.Complain;
    using ViewModel = OplachiSe.Web.Areas.Administration.Models.ComplainViewModel;

    public class ComplainController : KendoGridAdministrationController
    {

        public ComplainController(IOplachiSeData data)
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
                .Complains
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Complains.Find(id) as T;
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
                var complain = this.Data.Complains.Find(model.Id.Value);

                var comments = this.Data
                        .Comments
                        .All()
                        .Where(l => l.ComplainId == complain.Id)
                        .Select(l => l.Id)
                        .ToList();

                foreach (var commentId in comments)
                {
                    var likes = this.Data
                        .Likes
                        .All()
                        .Where(l => l.CommentId == commentId)
                        .Select(l => l.Id)
                        .ToList();

                    foreach (var likeId in likes)
                    {
                        this.Data.Likes.Delete(likeId);
                    }

                    this.Data.SaveChanges();

                    this.Data.Comments.Delete(commentId);
                }
                this.Data.SaveChanges();

                this.Data.Complains.Delete(complain);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

    }
}