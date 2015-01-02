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

    using Model = OplachiSe.Models.Like;
    using ViewModel = OplachiSe.Web.Areas.Administration.Models.LikeViewModel;

    public class LikeController : KendoGridAdministrationController
    {

        public LikeController(IOplachiSeData data)
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
                .Likes
                .All()
                .Project()
                .To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Likes.Find(id) as T;
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
                var like = this.Data.Likes.Find(model.Id.Value);

                this.Data.Likes.Delete(like);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

    }
}