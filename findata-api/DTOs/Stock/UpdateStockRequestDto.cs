using System.ComponentModel.DataAnnotations;

namespace findata_api.DTOs.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [MaxLength(25, ErrorMessage = "Company Name cannot be over 25 characters")]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [Range(1, 1000000000)]
    public decimal Purchase { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal LastDiv { get; set; }

    [Required]
    [MaxLength(25, ErrorMessage = "Industry Name cannot be over 25 characters")]
    public string Industry { get; set; } = string.Empty;

    [Range(1, 5000000000)]
    public long MarketCap { get; set; }
}
