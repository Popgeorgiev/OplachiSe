namespace OplachiSe.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string Extension { get; set; }

    }
}
