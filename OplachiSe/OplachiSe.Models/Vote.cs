namespace OplachiSe.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        public int ComplainId { get; set; }

        public virtual Complain Complain { get; set; }

        public string VoterId { get; set; }

        public virtual User Voter { get; set; }
    }
}
