#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;


namespace ConsoleApp1
{
    class Program
    {
        private static readonly HtmlWeb Loader = new HtmlWeb();
        private static readonly HttpClient Client = new HttpClient();
        private static readonly Stack<String> ListOfChannels = new Stack<string>();

        private const String ApiChannelId =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_id={0}";

        private const String ApiChannelName =
            "https://zen.yandex.ru/api/v3/launcher/export?_csrf=9831a76537b43259db7d8b0de06c7107291b4808-1572698342685&clid=300&country_code=ru&token=&channel_name={0}";

        private const ushort Pages = 10;

        private const ushort PullPages = 5;
        private const ushort PullChannels = 10;

        private static ushort _totalChannels;
        
        private static ushort _completedPages;

        private static ushort _currentPage = 1;

        private static ushort _pagesWorking;
        private static ushort _channelsWorking;
        
        private static readonly DateTime Start = DateTime.Now;

        private static void ParsePage(HtmlDocument doc)
        {
            foreach (var node in doc.DocumentNode.SelectNodes("//a[@class='channel-item__link']"))
            {
                ListOfChannels.Push(node.Attributes["href"].Value);
            }
        }

        private static async Task<ushort> LoadChannel(String channelName)
        {
            _totalChannels++;
            string urlToChannel;
            if (channelName.IndexOf("/id/", StringComparison.Ordinal) >= 0)
            {
                var channelId = channelName.Replace("/id/", "");
                urlToChannel = string.Format(ApiChannelId, channelId);
            }
            else
            {
                urlToChannel = string.Format(ApiChannelName, channelName.Substring(1));
            }
            await Client.GetStringAsync(urlToChannel);
            //var person = JsonSerializer.Deserialize<Person>(text); 
            //var file = new FileStream($"{person.rid}.json", FileMode.Create, FileAccess.Write);
            //file.Close();
            return 1;
        }

        private static async Task LoadPages()
        {
            List<Task<ushort>> tasks = new List<Task<ushort>>();
            while (true)
            {
                while (_pagesWorking < PullPages && (_completedPages + _pagesWorking) < Pages)
                {
                    var task = Loader.LoadFromWebAsync($"https://zen.yandex.ru/media/zen/channels?page={_currentPage}")
                        .ContinueWith((t) =>
                        {
                            ParsePage(t.Result);
                            return (ushort)2;
                        });
                    tasks.Add(task);
                    _currentPage++;
                    _pagesWorking++;
                }

                while (ListOfChannels.Count > 0 && _channelsWorking < PullChannels)
                {
                    var task = LoadChannel(ListOfChannels.Pop());
                    tasks.Add(task);
                    _channelsWorking++;
                }

                try
                {
                    var completedTask = await Task.WhenAny(tasks);
                    var result = await completedTask;
                    tasks.Remove(completedTask);
                    switch (result)
                    {
                        case 1:
                            _channelsWorking--;
                            break;
                        case 2:
                            _completedPages++;
                            _pagesWorking--;
                            break;
                    }
                    GC.Collect();
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    //Console.WriteLine(e.StackTrace);
                    break;
                }
            }
        }

        static async Task StartParsing()
        {
            Console.WriteLine("Старт начинаем! {0}", Start);
            await LoadPages();
            Console.WriteLine("При {0} страницах было получено {1} каналов {2}", Pages, _totalChannels, DateTime.Now - Start);
        }
        public static async Task Main()
        {
            await StartParsing();
        }
    }
}