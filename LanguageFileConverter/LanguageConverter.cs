using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LanguageFileConverter
{
    public static class LanguageConverter
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<XDocument> LoadAsync(string path)
        {
            var content = File.ReadAllText(path);

            var response = await client.PostAsync("http://converter:8080", new StringContent(content));

            var xmlStream = await response.Content.ReadAsStreamAsync();

            return XDocument.Load(xmlStream);
        }

        public static XDocument Load(string path)
        {
            var task = LoadAsync(path);

            task.Wait();

            return task.Result;
        }
    }
}