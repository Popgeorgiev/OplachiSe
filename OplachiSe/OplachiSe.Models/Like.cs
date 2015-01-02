namespace OplachiSe.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        [Key]
        public int Id { get; set; }

        public int Value { get; set; }

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string ByUserId { get; set; }

        public virtual User ByUser { get; set; }
    }
}
