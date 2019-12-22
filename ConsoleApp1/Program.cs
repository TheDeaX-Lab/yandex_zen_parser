#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using HtmlAgilityPack;


namespace ConsoleApp1
{
    class Program
    {
        private static HtmlWeb loader = new HtmlWeb();
        private static HttpClient client = new HttpClient();
        private static List<String> list_of_channels = new List<String>();

        private const String api_channel_id =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_id={0}";

        private const String api_channel_name =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_name={0}";

        private static UInt16 pages = 100;
        
        private static UInt16 pull_pages = 10;
        private static UInt16 pull_channels = 50;
        
        private static UInt16 _totalChannels = 0;
        
        private static UInt16 _completedPages = 0;
        private static UInt16 _completedChannels = 0;
        
        private static UInt16 _currentPage = 1;
        private static UInt16 _currentChannel = 1;
        
        private static UInt16 _pagesWorking = 0;
        private static UInt16 _channelsWorking = 0;
        
        private static DateTime start = DateTime.Now;
        static void ParsePage(HtmlDocument doc)
        {
            foreach (var node in doc.DocumentNode.SelectNodes("//a[@class='channel-item__link']"))
            {
                list_of_channels.Add(node.Attributes["href"].Value);
            }
        }

        static async Task<UInt16> LoadChannel(String channelName)
        {
            _totalChannels++;
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
            var text = await client.GetStringAsync(urlToChannel);
            //var person = JsonSerializer.Deserialize<Person>(text); 
            //var file = new FileStream($"{person.rid}.json", FileMode.Create, FileAccess.Write);
            //file.Close();
            return 1;
        }
        static async Task LoadPages()
        {
            List<Task<UInt16>> tasks = new List<Task<UInt16>>();
            while (true)
            {
                while (_pagesWorking < pull_pages && (_completedPages + _pagesWorking) < pages)
                {
                    var task = loader.LoadFromWebAsync($"https://zen.yandex.ru/media/zen/channels?page={_currentPage}")
                        .ContinueWith((t) =>
                        {
                            ParsePage(t.Result);
                            return (UInt16)2;
                        });
                    tasks.Add(task);
                    _currentPage++;
                    _pagesWorking++;
                }

                while (list_of_channels.Count > 0 && _channelsWorking < pull_channels)
                {
                    var task = LoadChannel(list_of_channels[0]);
                    list_of_channels.RemoveAt(0);
                    tasks.Add(task);
                    _channelsWorking++;
                    _currentChannel++;
                }

                try
                {
                    var completedTask = await Task.WhenAny(tasks);
                    var result = await completedTask;
                    tasks.Remove(completedTask);
                    switch (result)
                    {
                        case 1:
                            _completedChannels++;
                            _channelsWorking--;
                            break;
                        case 2:
                            _completedPages++;
                            _pagesWorking--;
                            break;
                    }
                    GC.Collect();
                }
                catch
                {
                    break;
                }
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Старт начинаем! {0}", start);
            await LoadPages();
            Console.WriteLine("При {0} страницах было получено {1} каналов {2}", pages, _totalChannels, DateTime.Now - start);
        }
    }
}