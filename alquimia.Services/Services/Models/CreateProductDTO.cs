using backendAlquimia.alquimia.Services.Services.Models;
using System.ComponentModel.DataAnnotations;

public class CreateProductoDTO
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string TipoProductoDescription { get; set; } = null!;

    [Required]
    public List<CreateProductVariantDTO> Variants { get; set; } = new();


}