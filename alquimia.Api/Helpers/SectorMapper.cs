namespace alquimia.Api.Helpers
{
    public static class SectorMapper
    {
        private static readonly Dictionary<string, string> _map = new(StringComparer.OrdinalIgnoreCase)
        {
            { "top", "Salida" },
            { "heart", "Corazón" },
            { "base", "Fondo" }
        };

        public static bool TryMapToSpanish(string englishSector, out string spanishSector)
        {
            return _map.TryGetValue(englishSector, out spanishSector);
        }

        public static List<string> ValidSectors => _map.Keys.ToList();
    }
}
