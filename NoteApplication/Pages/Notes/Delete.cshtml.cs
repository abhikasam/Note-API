using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NoteApplication.Models;
using NoteApplication.Models.Store;

namespace NoteApplication.Pages.Notes
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Note Note { get; set; }
        public async Task OnGet(int id)
        {
            var client = httpClientFactory.GetNoteApiClient();
            var response = await client.GetAsync("/api/note/" + id);
            var result=await response.Content.ReadAsStringAsync();
            Note=JsonConvert.DeserializeObject<Note>(result);   
        }

        public async Task<IActionResult> OnPost()
        {
            var client = httpClientFactory.GetNoteApiClient();
            var response = await client.DeleteAsync("/api/note/" + this.Note.NoteId);
            var result = await response.Content.ReadAsStringAsync();
            return RedirectToPage("/Notes/Index");
        }

    }
}
