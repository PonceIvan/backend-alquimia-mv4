namespace alquimia.Services.Helpers
{
    public class FormulaConcentration
    {
        public double Essence { get; set; }
        public double Alcohol { get; set; }
        public double Water { get; set; }

        public FormulaConcentration CalculateConcentrationBasedOnIntensity(int intensidadId)
        {
            return intensidadId switch
            {
                1 => new FormulaConcentration { Essence = 3, Alcohol = 70, Water = 27 },
                2 => new FormulaConcentration { Essence = 10, Alcohol = 82, Water = 8 },
                3 => new FormulaConcentration { Essence = 18, Alcohol = 80, Water = 2 },
                _ => throw new ArgumentException("Intensidad inválida. Solo se aceptan valores 1 (Baja), 2 (Media), 3 (Alta).")
            };
        }

    }

}
