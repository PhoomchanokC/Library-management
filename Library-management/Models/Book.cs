using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library_management.Models
{
    public class Book
    {
        [Key]
        [DisplayName("รหัสหนังสือ")]
        public string Id { get; set; }
        [Required]
        [DisplayName("ชื่อหนังสือ")]
        public string Title { get; set; }
        [Required]
        [DisplayName("รายระเอียด")]
        public string Description { get; set; }
        [Required]
        [DisplayName("ผู้เเต่ง")]
        public string Author { get; set; }
        [Required]
        [DisplayName("จำนวนการยืม")]
        public int Borrow_count { get; set; }
        [Required]
        [DisplayName("สถานะ")]
        public bool IS_borrow {  get; set; }
        public Book() { }

    }
}
