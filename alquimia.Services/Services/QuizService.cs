using alquimia.Data.Data.Entities;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Services.QuizLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alquimia.Services.Services
{
   
        public class QuizService : IQuizService
        {
        public QuizResponseDTO GetQuizResult(List<int> selectedOptions)
        {
            var familyScores = new Dictionary<string, int>();

            for (int i = 0; i < 9; i++) // Solo las primeras 9 preguntas definen familia
            {
                int questionId = i + 1;
                int optionIndex = selectedOptions[i];

                var mapping = FamilyMappingData.Mappings.FirstOrDefault(
                    m => m.QuestionId == questionId && m.OptionIndex == optionIndex);

                if (mapping != null)
                {
                    foreach (var fam in mapping.Families)
                        familyScores[fam] = familyScores.GetValueOrDefault(fam, 0) + 1;
                }
            }

            var topFamilies = familyScores.OrderByDescending(f => f.Value).Take(2).Select(f => f.Key).ToList();

            var families = topFamilies.Select(f => new QuizResultDTO
            {
                Family = f,
                CompatibleNotes = GetCompatibleNotes(f),
                Formula = GetExampleFormula(f)
            }).ToList();

            string concentration = selectedOptions[9] switch
            {
                1 => "Body Splash",
                2 => "Eau de Toilette",
                3 => "Eau de Parfum",
                _ => "Desconocido"
            };

            return new QuizResponseDTO
            {
                ConcentrationType = concentration,
                Families = families
            };
        }

        private List<string> GetCompatibleNotes(string family)
        {
            return family switch
            {
                "Floral" => new() { "Rosa", "Jazmín", "Ylang-Ylang", "Iris" },
                "Cítrico" => new() { "Bergamota", "Limón", "Mandarina", "Naranja" },
                "Amaderado" => new() { "Cedro", "Sándalo", "Vetiver", "Pachulí" },
                "Ámbar" => new() { "Ámbar Gris", "Canela", "Benjuí", "Haba tonka" },
                "Gourmand" => new() { "Caramelo", "Chocolate", "Vainilla", "Frambuesa" },
                "Terroso" => new() { "Pachulí", "Vetiver", "Musgo", "Haba tonka" },
                "Empolvado" => new() { "Talco", "Heliotropo", "Iris" },
                "Mentolado" => new() { "Menta Piperita", "Menta Verde", "Menta Poleo", "Eucalipto" },
                "Marino" => new() { "Algas", "Calone", "Ozono" },
                "Herbal" => new() { "Albahaca", "Tomillo", "Romero", "Salvia" },
                "Ahumado" => new() { "Cuero", "Tabaco", "Clavo" },
                "Almizclado" => new() { "Musk Blanco", "Ambrette" },
                "Aldehídico" => new() { "Aldehído C-12", "Aldehído C-14", "Aldehído C-16" },
                "Frutal" => new() { "Melocotón", "Manzana Verde", "Frambuesa", "Naranja" },
                "Fresca" => new() { "Lavanda", "Bergamota", "Menta Verde", "Ozono" },
                "Alcanforado" => new() { "Alcanfor", "Eucalipto", "Romero" },
                _ => new() { "Nota genérica" }
            };
        }


        private ExampleFormulaDTO GetExampleFormula(string family)
        {
            return family switch
            {
                "Floral" => new() { TopNote = "Bergamota", HeartNote = "Jazmín", BaseNote = "Ylang-Ylang" },
                "Cítrico" => new() { TopNote = "Limón", HeartNote = "Mandarina", BaseNote = "Vetiver" },
                "Amaderado" => new() { TopNote = "Pachulí", HeartNote = "Cedro", BaseNote = "Sándalo" },
                "Ámbar" => new() { TopNote = "Canela", HeartNote = "Haba tonka", BaseNote = "Ámbar Gris" },
                "Gourmand" => new() { TopNote = "Caramelo", HeartNote = "Chocolate", BaseNote = "Vainilla" },
                "Terroso" => new() { TopNote = "Vetiver", HeartNote = "Musgo", BaseNote = "Pachulí" },
                "Empolvado" => new() { TopNote = "Heliotropo", HeartNote = "Iris", BaseNote = "Talco" },
                "Mentolado" => new() { TopNote = "Menta Verde", HeartNote = "Menta Piperita", BaseNote = "Eucalipto" },
                "Marino" => new() { TopNote = "Calone", HeartNote = "Ozono", BaseNote = "Algas" },
                "Herbal" => new() { TopNote = "Romero", HeartNote = "Albahaca", BaseNote = "Salvia" },
                "Ahumado" => new() { TopNote = "Cuero", HeartNote = "Tabaco", BaseNote = "Clavo" },
                "Almizclado" => new() { TopNote = "Ambrette", HeartNote = "Musk Blanco", BaseNote = "Vetiver" },
                "Aldehídico" => new() { TopNote = "Aldehído C-12", HeartNote = "Aldehído C-14", BaseNote = "Aldehído C-16" },
                "Frutal" => new() { TopNote = "Manzana Verde", HeartNote = "Melocotón", BaseNote = "Frambuesa" },
                "Fresca" => new() { TopNote = "Lavanda", HeartNote = "Ozono", BaseNote = "Menta Verde" },
                "Alcanforado" => new() { TopNote = "Alcanfor", HeartNote = "Romero", BaseNote = "Eucalipto" },
                _ => new() { TopNote = "Nota X", HeartNote = "Nota Y", BaseNote = "Nota Z" }
            };
        }

    }
    }
