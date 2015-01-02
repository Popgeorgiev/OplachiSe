namespace OplachiSe.Web.Controllers
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OplachiSe.Data.Contracts;
    using OplachiSe.Web.Models;
    using OplachiSe.Web.Models.ComplainModels;

    public class HomeController : BaseController
    {
        public HomeController(IOplachiSeData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GetLatestComplains()
        {
            var latestComplains = this.Data
                .Complains
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .Take(10)
                .Project()
                .To<LatestComplainViewModel>()
                .ToList();

            return PartialView("_LatestComplainsPartial", latestComplains);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 10 * 60)]
        public ActionResult GetPopularComplains()
        {
            var complains = this.Data
                .Complains
                .All()
                .OrderByDescending(c => (c.Votes.Count + c.Comments.Count))
                .Project()
                .To<ComplainViewModel>()
                .Take(3)
                .ToList();

            return PartialView("_MostPopularComplainsPartial", complains);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}