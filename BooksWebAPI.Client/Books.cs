using BookModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooksWebAPI.Client
{
    public class Books : IDisposable
    {
        readonly HttpClient Client;

        public Books(string booksApiUri)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(booksApiUri);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(int skip = 0, int take = 25)
        {
            using (var response = await Client.GetAsync($"api/Books?skip={skip}&take={take}"))
            {
                if ((int)response.StatusCode >= 300)
                    throw new InvalidOperationException($"Response failed with code {response.StatusCode}");
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Book>>(content);
            }
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
