using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WealthZone.Dto.Comment;

namespace WealthZone.Dto.Stock
{
    public class StockDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [MinLength(3, ErrorMessage = "must be betweem 3 and 90")]
        [MaxLength(90, ErrorMessage = "must be betweem 3 and 90")]
        public string Symbol { get; set; } = string.Empty;

        [Required(ErrorMessage = "this field is required")]
        [MinLength(3, ErrorMessage = "must be betweem 3 and 90")]
        [MaxLength(90, ErrorMessage = "must be betweem 3 and 90")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0.1, 1000000, ErrorMessage ="this field is required and must be between 0.1 to 1000000")]
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentDto> comments { get; set; }
    }
}
