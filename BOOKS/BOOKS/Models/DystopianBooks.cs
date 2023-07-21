using System.ComponentModel.DataAnnotations;
namespace BOOKS.Models
{
    public class DystopianBooks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
