namespace OplachiSe.Web.Models.CommentModels
{
    using System.ComponentModel.DataAnnotations;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;
    using OplachiSe.Commons;

    public class AddCommentViewModel : IMapFrom<Comment>
    {
        [Required(ErrorMessage = GlobalConstants.RequiredErrorMessage)]
        [MinLength(5, ErrorMessage = GlobalConstants.MinLength)]
        [MaxLength(150, ErrorMessage = "Полето трябва да е под 150 символа")]
        [Display(Name = "Коментар")]
        [UIHint("CommentText")]
        public string Content { get; set; }

        public int ComplainId { get; set; }
    }
}