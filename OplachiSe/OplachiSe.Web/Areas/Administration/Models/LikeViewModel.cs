namespace OplachiSe.Web.Areas.Administration.Models
{
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    public class LikeViewModel : IMapFrom<Like>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public string AuthorName { get; set; }

        public int Value { get; set; }

        public int CommentId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Like, LikeViewModel>()
                .ForMember(m => m.AuthorName, opt => opt.MapFrom(l => l.ByUser.UserName));
        }
    }
}