using System.ComponentModel.DataAnnotations;

public class CreateProductoDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Required]
    public string TipoProductoDescription { get; set; } // "Esencias", "Envases", etc.
}