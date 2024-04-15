using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_management.Models
{
  
    public class Borrow_log
    {

        [Required]
        [Key]
        public string borrow_id{get; set;}
                
        [Required]
        public string book_id { get; set;}

        [Required]
        public string userid { get; set;}

        [Required]
        public string start {  get; set;}
        
        [Required]
        public string end { get; set;}

      
    }
}
