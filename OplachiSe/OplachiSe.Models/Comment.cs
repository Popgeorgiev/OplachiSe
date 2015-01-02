namespace OplachiSe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        private ICollection<Like> likes;

        public Comment()
        {
            this.likes = new HashSet<Like>();
        }

        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int ComplainId { get; set; }

        public virtual Complain Complain { get; set; }

        public virtual ICollection<Like> Likes 
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
