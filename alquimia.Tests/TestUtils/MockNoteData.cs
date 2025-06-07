using alquimia.Data.Entities;

namespace alquimia.Tests.TestUtils
{
    public static class MockNoteData
    {
        public static IQueryable<Note> GetMockNotes()
        {
            return new List<Note>
            {
                new Note
                {
                    Id = 47,
                    Name = "Vetiver",
                    Description = "Raíces secas",
                    OlfactoryFamily = new OlfactoryFamily { Nombre = "Terroso", Description = "Húmedo y natural, tierra mojada, raíces." },
                    OlfactoryPyramid = new OlfactoryPyramid { Sector = "Fondo", Duracion = new TimeOnly(6, 0)}
                },
                new Note
                {
                    Id = 40,
                    Name = "Romero",
                    Description = "Verde limpio silvestre",
                    OlfactoryFamily = new OlfactoryFamily { Nombre = "Hierbas aromáticas", Description = "Notas frescas y verde, romero, lavanda." },
                    OlfactoryPyramid = new OlfactoryPyramid { Sector = "Corazón", Duracion = new TimeOnly(1, 0) }
                },
                new Note
                {
                    Id = 23,
                    Name = "Naranja",
                    Description = "Aroma dulce y cítrico",
                    OlfactoryFamily = new OlfactoryFamily { Nombre = "Cítrico", Description = "Refrescante y chispeante." },
                    OlfactoryPyramid = new OlfactoryPyramid { Sector = "Salida", Duracion = new TimeOnly(0, 15) }
                },
                new Note
                {
                    Id = 1,
                    Name = "Cedro",
                    Description = "Madera seca y suave",
                    OlfactoryFamily = new OlfactoryFamily { Nombre = "Amaderado", Description = "Notas secas y cálidas de madera." },
                    OlfactoryPyramid = new OlfactoryPyramid { Sector = "Fondo", Duracion = new TimeOnly(6, 0) }
                },
            }.AsQueryable();
        }
    }
}
