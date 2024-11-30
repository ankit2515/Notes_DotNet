using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.api.Data;
using Notes.api.Model.Domain;
using Notes.api.Model.DTO;

namespace Notes.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteDbContext dbContext;
        public NotesController(NoteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetNotes() {
            var note= dbContext.Notes.ToList();
            var noteDto = new List<Model.DTO.displayNote>();

            foreach (var item in note) {
                noteDto.Add(new Model.DTO.displayNote
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    
                    DateCreated = item.DateCreated
                });
                
            }
            return Ok(noteDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetNotebyId(Guid id) {
            var note = dbContext.Notes.Find(id);
            if (note is not null) {
                var noteDto = new Model.DTO.displayNote
                {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    DateCreated = note.DateCreated
                };
                return Ok(noteDto);
            }       
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddNote(NoteDTO noteDto) {
            //convert DTO to domain model
            var note = new Note {
                Title = noteDto.Title,
                Description = noteDto.Description,
            };
            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
            return Ok(note);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateNote(Guid id, UpdateNoteDto noteDto) { 
          var note = dbContext.Notes.Find(id);
            if (note is not null) {
                 note.Title = noteDto.Title;    
                note.Description = noteDto.Description; 


                dbContext.SaveChanges();
                return Ok(note);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteNote(Guid id) {
            var notes = dbContext.Notes.Find(id);
            if (notes is not null)
            {
                dbContext.Notes.Remove(notes);
                dbContext.SaveChanges();
                return Ok("Deleted Successfully");

            }
            return BadRequest();
        }
    }
}
