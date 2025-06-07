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

        public static List<NotesGroupedByFamilyDTO> GetTopNotesGrouped()
        {
            return new List<NotesGroupedByFamilyDTO>
            {
                new NotesGroupedByFamilyDTO
                {
                    Family = "Cítrico",
                    Notes = new List<NoteDTO>
                    {
                        new NoteDTO
                        {
                            Id = 23,
                            Name = "Naranja",
                            Family = "Cítrico",
                            Sector = "Salida",
                            Description = "Aroma dulce y cítrico",
                            Duration = new TimeOnly(0, 15)
                        }
                    }
                },
                new NotesGroupedByFamilyDTO
                {
                    Family = "Alcanforado",
                    Notes = new List<NoteDTO>
                    {
                        new NoteDTO
                        {
                            Id = 7,
                            Name = "Eucalipto",
                            Family = "Alcanforado",
                            Sector = "Salida",
                            Description = "Fresco herbáceo",
                            Duration = new TimeOnly(0, 15)
                        }
                    }
                },
            };
        }

        public static List<NotesGroupedByFamilyDTO> GetHeartNotesGrouped()
        {
            return new List<NotesGroupedByFamilyDTO>
            {
                new NotesGroupedByFamilyDTO
                {
                    Family = "Frutal",
                    Notes = new List<NoteDTO>
                    {
                        new NoteDTO
                        {
                            Id = 36,
                            Name = "Frambuesa",
                            Family = "Frutal",
                            Sector = "Corazón",
                            Description = "Afrutado jugoso",
                            Duration = new TimeOnly(1, 0)
                        }
                    }
                },
                new NotesGroupedByFamilyDTO
                {
                    Family = "Especiado",
                    Notes = new List<NoteDTO>
                    {
                        new NoteDTO
                        {
                            Id = 29,
                            Name = "Clavo",
                            Family = "Especiado",
                            Sector = "Corazón",
                            Description = "Amaderado intenso",
                            Duration = new TimeOnly(1, 0)
                        }
                    }
                },
            };
        }
    }
}
