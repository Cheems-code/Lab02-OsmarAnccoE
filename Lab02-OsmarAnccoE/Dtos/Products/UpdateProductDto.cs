using System.ComponentModel.DataAnnotations;

namespace Lab02_OsmarAnccoE.Dtos.Products;

public class UpdateProductDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Range(0.01, 10000)]
    public decimal Price { get; set; }
}