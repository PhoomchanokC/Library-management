using System.ComponentModel.DataAnnotations;

namespace Library_management.Models
{
    public class Book
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Borrow_count { get; set; }
        [Required]
        public bool IS_borrow {  get; set; }
        public Book() { }

    }
}
