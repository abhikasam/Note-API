namespace NoteApplication.Models
{
    public static class IHttpClientFactoryExt
    {
        public static HttpClient GetNoteApiClient(this IHttpClientFactory httpClientFactory)
        {
            return httpClientFactory.CreateClient("NoteApi");
        }
    }
}
