using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NoteApplication.Models;
using NoteApplication.Models.Store;
using System.Text.Json.Serialization;

namespace NoteApplication.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public List<Note> Notes { get; set; }
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGet()
        {
            HttpClient client = httpClientFactory.GetNoteApiClient();
            var response = await client.GetAsync("api/note");
            var responseResult = await response.Content.ReadAsStringAsync();
            this.Notes=JsonConvert.DeserializeObject<List<Note>>(responseResult);   
        }
    }
}
