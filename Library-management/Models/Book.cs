using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_management.Models
{
    public class Book
    {
        [Key]
        [Required]
        [DisplayName("รหัสหนังสือ")]
        public string Id { get; set; }
        
        [Required]
        [DisplayName("ชื่อหนังสือ")]
        public string Title { get; set; }
        
        [Required]
        [DisplayName("รายระเอียด")]
        public string Description { get; set; }
        
        [Required]
        [DisplayName("หมวดหมู่")]
        public string Category { get; set; }
        
        [Required]
        [DisplayName("ผู้เเต่ง")]
        public string Author { get; set; }

        [Required]
        [DisplayName("รูปปกหนังสือ")]
        public string Image { get; set; }

        [Required]
        [DisplayName("จำนวนการยืม")]
        public int Borrow_count { get; set; }
        
        [Required]
        [DisplayName("สถานะ")]
        public bool IS_borrow {  get; set; }
        
        [DisplayName("ยืมตั่งเเต่")]
        public DateTime Start { get; set; }
        
        [DisplayName("ยืมถึง")]
        public DateTime Stop { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Book() { }

    }
}
