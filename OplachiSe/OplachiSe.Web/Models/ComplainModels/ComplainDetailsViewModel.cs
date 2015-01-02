namespace OplachiSe.Web.Models.ComplainModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;
    using OplachiSe.Web.Models.CommentModels;

    public class ComplainDetailsViewModel : IMapFrom<Complain>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public int? PictureId { get; set; }

        public double Score { get; set; }


        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Complain, ComplainDetailsViewModel>()
                .ForMember(c => c.AuthorName, opt => opt.MapFrom(c => c.Author.UserName))
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(c => c.Category.Name));
        }
    }
}