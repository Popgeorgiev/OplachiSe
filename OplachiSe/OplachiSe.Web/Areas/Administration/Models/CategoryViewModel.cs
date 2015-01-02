namespace OplachiSe.Web.Areas.Administration.Models
{
    using AutoMapper;
    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class CategoryViewModel : IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}