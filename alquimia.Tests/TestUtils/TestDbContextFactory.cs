using alquimia.Data.Entities;
using Microsoft.EntityFrameworkCore;

public static class TestDbContextFactory
{
    public static AlquimiaDbContext CreateContextWithNotes(List<Note> notes)
    {
        var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
            .UseInMemoryDatabase(databaseName: "AlquimiaTestDb")
            .Options;

        var context = new AlquimiaDbContext(options);
        context.Notes.AddRange(notes);
        context.SaveChanges();
        return context;
    }
}
