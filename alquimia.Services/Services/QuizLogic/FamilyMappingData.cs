using System.Collections.Generic;
namespace backendAlquimia.alquimia.Services.Services.QuizLogic
    {
        public static class FamilyMappingData
        {
            public static List<FamilyMapping> Mappings => new List<FamilyMapping>
        {
            new() { QuestionId = 1, OptionIndex = 1, Families = new() { "Fresco", "Floral" } },
            new() { QuestionId = 1, OptionIndex = 2, Families = new() { "Floral", "Cítrico" } },
            new() { QuestionId = 1, OptionIndex = 3, Families = new() { "Ámbar", "Gourmand" } },
            new() { QuestionId = 1, OptionIndex = 4, Families = new() { "Amaderado", "Especiado" } },

            new() { QuestionId = 2, OptionIndex = 1, Families = new() { "Ámbar", "Amaderado" } },
            new() { QuestionId = 2, OptionIndex = 2, Families = new() { "Marino", "Floral" } },

            new() { QuestionId = 3, OptionIndex = 1, Families = new() { "Fresca", "Cítrico", "Aldehídico" } },
            new() { QuestionId = 3, OptionIndex = 2, Families = new() { "Floral", "Almizclado" } },
            new() { QuestionId = 3, OptionIndex = 3, Families = new() { "Ámbar", "Especiado" } },
            new() { QuestionId = 3, OptionIndex = 4, Families = new() { "Amaderado", "Terroso" } },

            new() { QuestionId = 4, OptionIndex = 1, Families = new() { "Fresca", "Cítrico", "Marino", "Frutal" } },
            new() { QuestionId = 4, OptionIndex = 2, Families = new() { "Floral", "Herbal" } },
            new() { QuestionId = 4, OptionIndex = 3, Families = new() { "Amaderado", "Especiado" } },
            new() { QuestionId = 4, OptionIndex = 4, Families = new() { "Ámbar", "Almizclado" } },

            new() { QuestionId = 5, OptionIndex = 1, Families = new() { "Floral" } },
            new() { QuestionId = 5, OptionIndex = 2, Families = new() { "Amaderado", "Terroso" } },
            new() { QuestionId = 5, OptionIndex = 3, Families = new() { "Gourmand", "Frutal" } },
            new() { QuestionId = 5, OptionIndex = 4, Families = new() { "Mentolado", "Herbal" } },

            new() { QuestionId = 6, OptionIndex = 1, Families = new() { "Herbal", "Mentolado" } },
            new() { QuestionId = 6, OptionIndex = 2, Families = new() { "Ámbar", "Gourmand", "Especiado" } },
            new() { QuestionId = 6, OptionIndex = 3, Families = new() { "Floral" } },
            new() { QuestionId = 6, OptionIndex = 4, Families = new() { "Fresca", "Marino", "Mentolado" } },

            new() { QuestionId = 7, OptionIndex = 1, Families = new() { "Herbal", "Terroso", "Amaderado" } },
            new() { QuestionId = 7, OptionIndex = 2, Families = new() { "Ámbar", "Empolvado", "Gourmand" } },
            new() { QuestionId = 7, OptionIndex = 3, Families = new() { "Fresca", "Mentolado", "Marino", "Cítrico", "Aldehídico" } },
            new() { QuestionId = 7, OptionIndex = 4, Families = new() { "Floral", "Almizclado" } },

            new() { QuestionId = 8, OptionIndex = 1, Families = new() { "Fresca", "Cítrico", "Mentolado" } },
            new() { QuestionId = 8, OptionIndex = 2, Families = new() { "Ámbar", "Especiado" } },
            new() { QuestionId = 8, OptionIndex = 3, Families = new() { "Floral", "Almizclado" } },
            new() { QuestionId = 8, OptionIndex = 4, Families = new() { "Amaderado", "Ahumado" } },

            new() { QuestionId = 9, OptionIndex = 1, Families = new() { "Ámbar", "Especiado", "Gourmand" } },
            new() { QuestionId = 9, OptionIndex = 2, Families = new() { "Floral", "Empolvado", "Aldehídico" } },
            new() { QuestionId = 9, OptionIndex = 3, Families = new() { "Fresca", "Herbal", "Mentolado" } },
            new() { QuestionId = 9, OptionIndex = 4, Families = new() { "Amaderado", "Almizclado" } }
        };
        }
    }

