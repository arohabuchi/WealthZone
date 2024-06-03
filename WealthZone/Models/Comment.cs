using System.ComponentModel.DataAnnotations.Schema;

namespace WealthZone.Models
{
    [Table("comments")]
    public class Comment
    {
        
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty ;
        public DateTime CreatedOn { get; set; } = DateTime.Now; 
        public int? StockId { get; set; }
        public Stock? stock { get; set; }
        //one to one with user
        public string AppUserId { get; set; }
        public ApplicationUser AppUser { get; set;}
    }
}
