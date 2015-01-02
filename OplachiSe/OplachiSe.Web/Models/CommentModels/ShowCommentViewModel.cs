namespace OplachiSe.Web.Models.CommentModels
{
    using System;
    using System.Linq;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    public class ShowCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime? CreatedOn { get; set; }


        public int? LikesResult { get; set; }



        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Comment, ShowCommentViewModel>()
                .ForMember(c => c.AuthorName, opt => opt.MapFrom(c => c.Author.UserName));

           configuration.CreateMap<Comment, ShowCommentViewModel>()
                .ForMember(c => c.LikesResult, opt => opt.MapFrom(c => c.Likes.Select(s => s.Value).Sum()));
        }
    }
}