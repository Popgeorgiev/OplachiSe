
namespace OplachiSe.Web.Models.VoteModels
{
    using OplachiSe.Models;
    using OplachiSe.Web.Infrastructure.Mapping;

    using System.ComponentModel.DataAnnotations;

    public class NewVoteViewModel : IMapFrom<Vote>
    {
        [Display(Name = "Оценка")]
        [UIHint("VoteResult")]
        public int Value { get; set; }

        public int ComplainId { get; set; }
    }
}