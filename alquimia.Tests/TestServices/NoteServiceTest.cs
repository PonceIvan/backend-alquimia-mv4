using alquimia.Services;
using alquimia.Tests.TestUtils;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class NoteServiceTest
    {
        [Fact]
        public async Task GetBaseNotesGroupedByFamilyAsync_ShouldReturnNotesGroupedByFamilyAsync()
        {
            var context = TestDbContextFactory.CreateContextWithNotes(MockNoteData.GetMockNotes().ToList());

            var service = new NoteService(context);

            // Act
            var result = await service.GetBaseNotesGroupedByFamilyAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.DoesNotContain(result, g => g.Family == "Cítrico");
            Assert.DoesNotContain(result, g => g.Family == "Hierbas aromáticas");
            Assert.Contains(result, g => g.Family == "Terroso");
            Assert.Contains(result, g => g.Family == "Amaderado");
        }
    }
}
