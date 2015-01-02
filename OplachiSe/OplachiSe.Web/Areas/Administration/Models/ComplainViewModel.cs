namespace OplachiSe.Web.Areas.Administration.Models
{
    using AutoMapper;
    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class ComplainViewModel : IMapFrom<Complain>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public int VotesCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Complain, ComplainViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(l => l.Author.UserName))
                .ForMember(m => m.VotesCount, opt => opt.MapFrom(l => l.Votes.Count));
        }
    }
}