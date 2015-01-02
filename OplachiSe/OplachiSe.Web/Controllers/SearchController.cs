namespace OplachiSe.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using OplachiSe.Web.Models.ComplainModels;
    using OplachiSe.Data.Contracts;

    public class SearchController : BaseController
    {
        public SearchController(IOplachiSeData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Find(string search, int? category)
        {
            string word;
            if (search == null)
            {
                word = string.Empty;
            }
            else
            {
                word = search.ToLower();
            }

            var foundComplains = this.Data
                .Complains
                .All()
                .Where(c => ((c.Title.ToLower().Contains(word) || c.Content.ToLower().Contains(word)) && c.CategoryId == category))
                .Project()
                .To<ComplainSearchViewModel>()
                .ToList();

            var findView = new ComplainFindViewModel()
            {
                Complains = foundComplains,
                Categories = new SelectList(this.Data.Categories.All().ToList(), "Id", "Name")
            };

            return View(findView);
        }

        [OutputCache(Duration = 5 * 60)]
        public ActionResult Display(int page = 1 )
        {
            var allComplains = this.Data
                .Complains
                .All();

            var complainsCount = allComplains.Count();

            var pageNumber = complainsCount / 5;
            if (complainsCount % 5 != 0 || pageNumber == 0)
            {
                pageNumber++;
            }

            if (page < 1)
            {
                page = 1;
            }
            else if (page > pageNumber)
            {
                page = pageNumber;
            }

            var complainsOnPage = allComplains
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * 5)
                .Take(5)
                .Project()
                .To<ComplainSearchViewModel>()
                .ToList();

            var pager = new ComplainPagerViewModel()
            {
                Complains = complainsOnPage,
                CurrentPage = page,
                MaxPages = pageNumber,
                Categories = new SelectList(this.Data.Categories.All().ToList(), "Id", "Name")
            };

            return this.View(pager);
        }
    }
}