using System.ComponentModel.DataAnnotations.Schema;

namespace WealthZone.Models
{
    [Table("portfolios")]

    public class Portfolio
    {
        public int StockId { get; set; }
        public Stock stock { get; set; }
        public string AppUserId { get; set; }
        public ApplicationUser appUser { get; set;}
    }
    
}
