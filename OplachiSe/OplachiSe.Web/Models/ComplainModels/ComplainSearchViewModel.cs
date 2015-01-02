namespace OplachiSe.Web.Models.ComplainModels
{
    using System;
    using AutoMapper;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    public class ComplainSearchViewModel : IMapFrom<Complain>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? PictureId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Complain, ComplainDetailsViewModel>()
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(c => c.Category.Name));
        }
    }
}