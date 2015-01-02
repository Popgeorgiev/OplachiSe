namespace OplachiSe.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Complain
    {
        private ICollection<Comment> comments;
        private ICollection<Vote> votes;

        public Complain()
        {
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(300)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int? PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; } 
            set { this.comments = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public DateTime CreatedOn { get; set; }


        public double CalculateVoteScore()
        {
            if (this.Votes.Count != 0)
            {
                double sum = this.Votes.Sum(s => s.Value);
                return sum / votes.Count();
            }

            return 0;
        }
    }
}
