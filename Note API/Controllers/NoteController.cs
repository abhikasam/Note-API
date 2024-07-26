using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note_API.Models.Store;

namespace Note_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly StoreContext storeContext;

        public NoteController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(storeContext.Notes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var note = storeContext.Notes.Find(id);
            if(note!=null)
                return Ok(note);
            return Ok(new Note());
        }

        [HttpPost]
        public IActionResult Post([FromBody]string content)
        {
            var note = new Note()
            {
                Content = content
            };
            storeContext.Notes.Add(note);
            storeContext.SaveChanges();
            return Ok(note);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string content)
        {
            var note = storeContext.Notes.Find(id);
            note.Content = content;
            storeContext.SaveChanges();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = storeContext.Notes.Find(id);
            storeContext.Notes.Remove(note);
            storeContext.SaveChanges();
            return Ok(note);
        }

    }
}
