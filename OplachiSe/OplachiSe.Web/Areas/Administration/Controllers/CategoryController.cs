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

    using Model = OplachiSe.Models.Category;
    using ViewModel = OplachiSe.Web.Areas.Administration.Models.CategoryViewModel;

    public class CategoryController : KendoGridAdministrationController
    {

        public CategoryController(IOplachiSeData data)
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
                .Categories
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Categories.Find(id) as T;
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
                var category = this.Data.Categories.Find(model.Id.Value);

                var complains = this.Data
                        .Complains
                        .All()
                        .Where(l => l.CategoryId == category.Id)
                        .Select(l => l.Id)
                        .ToList();

                foreach (var complainId in complains)
                {
                    var comments = this.Data
                        .Comments
                        .All()
                        .Where(l => l.ComplainId == complainId)
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
                    this.Data.Complains.Delete(complainId);
                    this.Data.SaveChanges();
                }
                

                this.Data.Categories.Delete(category);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

    }
}