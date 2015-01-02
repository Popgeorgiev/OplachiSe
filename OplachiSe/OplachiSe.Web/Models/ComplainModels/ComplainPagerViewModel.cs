namespace OplachiSe.Web.Models.ComplainModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    public class ComplainPagerViewModel
    {
        public int CurrentPage { get; set; }

        public int MaxPages { get; set; }

        public string SearchWord { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public ICollection<ComplainSearchViewModel> Complains { get; set; }
    }
}