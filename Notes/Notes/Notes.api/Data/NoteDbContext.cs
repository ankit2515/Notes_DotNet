using Microsoft.EntityFrameworkCore;
using Notes.api.Model.Domain;

namespace Notes.api.Data
{
    public class NoteDbContext: DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options):base(options)
        {
            
        }

        public DbSet<Note> Notes { get; set; }
    }
}
