using alquimia.Services.Models;

namespace alquimia.Tests.TestUtils
{
    public static class MockGroupedNotesDataDTO
    {
        public static List<NotesGroupedByFamilyDTO> GetBaseNotesGrouped()
        {
            return new List<NotesGroupedByFamilyDTO>
            {
                new NotesGroupedByFamilyDTO
                {
                    Family = "Amaderado",
                    Notes = new List<NoteDTO>
                    {
                        new NoteDTO
                        {
                            Id = 1,
                            Name = "Cedro",
                            Family = "Amaderado",
                            Sector = "Fondo",
                            Description = "Notas secas y cálidas de madera.",
                            Duration = new TimeOnly(6, 0)
                        }
                    }
                },
                new NotesGroupedByFamilyDTO
                {
                    Family = "Terroso",
                    Notes = new List<NoteDTO>
                    {
                        new NoteDTO
                        {
                            Id = 47,
                            Name = "Vetiver",
                            Family = "Terroso",
                            Sector = "Fondo",
                            Description = "Húmedo y natural, tierra mojada, raíces.",
                            Duration = new TimeOnly(6, 0)
                        }
                    }
                },
            };
        }
    }
}
