using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthZone.Dto.Stock
{
    public class CreateStockRequestDto
    {
        [Required(ErrorMessage = "this field is required")]
        [MinLength(3, ErrorMessage = "must be betweem 3 and 90")]
        [MaxLength(90, ErrorMessage = "must be betweem 3 and 90")]
        public string Symbol { get; set; } = string.Empty;
        [Required(ErrorMessage = "this field is required")]
        [MinLength(3, ErrorMessage = "must be betweem 3 and 90")]
        [MaxLength(90, ErrorMessage = "must be betweem 3 and 90")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0.1, 9000000, ErrorMessage = "must be betweem 3 and 9000000")]
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
