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

        private const String api_channel_id =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_id={0}";

        private const String api_channel_name =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_name={0}";
        private static UInt16 pages = 25;
        private static UInt16 pull_pages = 1;
        private static UInt16 totalChannels = 0;
        private static DateTime start = DateTime.Now;
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
                urlToChannel = String.Format(api_channel_name, channelName.Substring(1));
            }
            try
            {
                var request = await client.GetAsync(urlToChannel);
                var text = await request.Content.ReadAsStringAsync();
                var person = JsonSerializer.Deserialize<Person>(text);
            }
            catch
            {
                Console.WriteLine("Ошибка!!! Не удалось получить {0}", urlToChannel);
            }
        }
        static async Task LoadPages()
        {
            for (var i = 0; i < pages / pull_pages; i++)
            {
                var tasks = new List<Task>();
                for (var k = pull_pages*i+1; k <= pull_pages*(i+1); k++)
                {
                    var task = loader.LoadFromWebAsync($"https://zen.yandex.ru/media/zen/channels?page={k}");
                    tasks.Add(Task.Run(() => task
                        .ContinueWith(async t => await ParsePage(t.Result))
                        .Unwrap()));
                }

                await Task.WhenAll(tasks);
                Console.WriteLine("{1} Партия закончилась: {0} секунд", DateTime.Now - start, i+1);
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Старт начинаем! {0}", start);
            await LoadPages();
            Console.WriteLine("При {0} страницах было получено {1} каналов, среднее время на партию: {2}", pages, totalChannels, (DateTime.Now - start).TotalSeconds/pages);
        }
    }
}