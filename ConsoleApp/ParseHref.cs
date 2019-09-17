using System;
using System.Net.Http;
using System.Threading;
using AngleSharp.Html.Parser;
using System.IO;
using System.Collections.Generic;
using Parser.Properties;

namespace ConsoleApp3
{
    class ParseHref
    {
        public int End, Start;
        string[] list;
        public async void ParserHref(int Start, int End)
        {
            Console.WriteLine("Parsing Started...");
            var List = new List<string>();
            for (int i = Start; i <= End; i++)
            {
                Console.WriteLine("...Parsing of page" + i + "...");
                int id = i;
                var BaseurL = Resources.MyUrl;
                var uri = BaseurL.Replace("{x}", Convert.ToString(id));
                var cancellationToken = new CancellationTokenSource();
                var httpClient = new HttpClient();
                var request = await httpClient.GetAsync(uri);
                cancellationToken.Token.ThrowIfCancellationRequested();

                var response = await request.Content.ReadAsStreamAsync();
                cancellationToken.Token.ThrowIfCancellationRequested();

                var parser = new HtmlParser();
                var document = parser.ParseDocument(response);

                var pricesListItemsLinq = document.QuerySelectorAll("a.item-card__name");
                foreach (var item in pricesListItemsLinq)
                {
                    List.Add(item.Attributes["href"].Value);
                }
                list = List.ToArray();
                StreamWriter sw = new StreamWriter(@"C:\Users\aizhi\Desktop\parsed.txt");
                foreach (string line in list)
                {
                    sw.WriteLine(line);
                }
                sw.Close();
            }
            Console.WriteLine("Parsing of href Completed!");
        }
    }
}