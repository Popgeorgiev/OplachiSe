namespace OplachiSe.Web.Models
{
    using System;

    using AutoMapper;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    public class ComplainViewModel : IMapFrom<Complain>
    {
        public ComplainViewModel()
        {
            
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int? PictureId { get; set; }
    }
}