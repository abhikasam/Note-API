using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NoteApplication.Models;
using NoteApplication.Models.Store;

namespace NoteApplication.Pages.Notes
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public Note Note { get; set; }
        public EditModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGet(int id)
        {
            var client = httpClientFactory.GetNoteApiClient();
            var response = await client.GetAsync("/api/note/"+id);
            var responseResult=await response.Content.ReadAsStringAsync();
            this.Note=JsonConvert.DeserializeObject<Note>(responseResult);
        }

        public async Task OnPost()
        {
            var client = httpClientFactory.GetNoteApiClient();
            var request = JsonConvert.SerializeObject(Note.Content);
            using(var content=new StringContent(request, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage response;
                if (Note.NoteId != 0)
                {
                    response = await client.PutAsync("/api/note/" + Note.NoteId, content);
                }
                else
                {
                    response = await client.PostAsync("/api/note/", content);
                }
                var responseResult = await response.Content.ReadAsStringAsync();
                Note = JsonConvert.DeserializeObject<Note>(responseResult);
            }
        }

    }
}
