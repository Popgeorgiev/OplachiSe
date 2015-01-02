namespace OplachiSe.Web.Models.ComplainModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;
    using OplachiSe.Commons;

    public class CreateComplainViewModel : IMapFrom<Complain>
    {
        public CreateComplainViewModel()
        {
        }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMessage)]
        [MinLength(5, ErrorMessage = GlobalConstants.MinLength)]
        [MaxLength(40, ErrorMessage = "Полето трябва да е под 40 символа")]
        [UIHint("SingleLineText")]
        [Display(Name ="Заглавие")]
        
        public string Title { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredErrorMessage)]
        [MinLength(20, ErrorMessage = GlobalConstants.MinLength20)]
        [MaxLength(300, ErrorMessage = "Полето трябва да е под 300 символа")]
        [Display(Name ="Съдържание")]
        [UIHint("MultiLineText")]
        public string Content { get; set; }

        [Display(Name = "Категория")]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }
            
    }
}