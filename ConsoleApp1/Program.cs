#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.Json;


namespace ConsoleApp1
{

    class Program
    {
        private static HtmlWeb loader = new HtmlWeb();
        private static HttpClient client = new HttpClient();
        private static UInt64 file_number = 0;

        private const String api_channel_id =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_id={0}";

        private const String api_channel_name =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_name={0}";
        private static UInt16 pages = 100;
        private static UInt16 totalChannels = 0;
        static async Task ParsePage(HtmlDocument doc)
        {
            var tasks = new List<Task>();
            foreach (var node in doc.DocumentNode.SelectNodes("//a[@class='channel-item__link']"))
            {
                var task = LoadChannel(node.Attributes["href"].Value);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }
        
        static async Task LoadChannel(String channelName)
        {
            totalChannels++;
            var urlToChannel = "";
            if (channelName.IndexOf("id/", StringComparison.Ordinal) >= 0)
            {
                var channelId = channelName.Replace("/id/", "");
                urlToChannel = String.Format(api_channel_id, channelId);
            }
            else
            {
                urlToChannel = String.Format(api_channel_name, channelName);
            }

            try
            {
                var request = await client.GetAsync(urlToChannel);
                var text = await request.Content.ReadAsStringAsync();

                try
                {
                    var person = JsonSerializer.Deserialize<Person>(text);

                    foreach (var article in person.items)
                    {
                        String similar, pixels;
                        similar = article.similar.ToString();
                        pixels = article.pixels.ToString();
                        if (similar != "{}" && similar != "" || pixels != "[]" && pixels !="" ) Console.WriteLine("Симиляр: {0}\nПиксели: {1}", article.similar.ToString(), article.pixels.ToString());
                    }
                }
                catch
                {
                    var dict = JsonSerializer.Deserialize<Dictionary<String, JsonElement?>>(text);
                    if (dict["error"].ToString() != "1") Console.WriteLine("Ошибка!!! {0}", text);
                }
            }
            catch
            {
                Console.WriteLine("Ошибка!!! Не удалось получить {0}", urlToChannel);
            }
        }
        static async Task LoadPages()
        {
            var tasks = new List<Task>();
            for(var k = 1; k <= pages; k++) {
                var task = loader.LoadFromWebAsync($"https://zen.yandex.ru/media/zen/channels?page={k}");
                tasks.Add(Task.Run(() => task
                    .ContinueWith(async t => await ParsePage(t.Result))
                    .Unwrap()));
            }
            await Task.WhenAll(tasks);
        }
        
        static async Task Main(string[] args)
        {
            await LoadPages();
            Console.WriteLine("При {0} страницах было получено {1} каналов", pages, totalChannels);
        }
    }
}