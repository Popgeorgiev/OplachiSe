namespace OplachiSe.Web.Models.ComplainModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ComplainFindViewModel
    {
        public string SearchWord { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public ICollection<ComplainSearchViewModel> Complains { get; set; }
    }
}