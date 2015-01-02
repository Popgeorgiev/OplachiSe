namespace OplachiSe.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [UIHint("String")]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}