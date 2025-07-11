﻿using System.ComponentModel.DataAnnotations;

namespace alquimia.Services.Models
{
    public class CreateProductVariantDTO
    {
        public decimal Volume { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string? Image { get; set; } = null!;
        public bool? IsHypoallergenic { get; set; }
        public bool? IsVegan { get; set; }
        public bool? IsParabenFree { get; set; }
    }
}
