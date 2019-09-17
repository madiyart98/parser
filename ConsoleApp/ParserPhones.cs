using System;
using System.Linq;
using System.Net.Http;
using AngleSharp.Html.Dom;
using System.Threading.Tasks;
using System.Threading;
using AngleSharp.Html.Parser;
using AngleSharp.Browser.Dom;
using System.IO;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
namespace ConsoleApp3
{
    class ParsePhones
    {
        public async void ParserPhones()
        {
            string[] href = File.ReadAllLines(@"C:\Users\aizhi\Desktop\parsed.txt");
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("Kaspi_Store");
            var collection = db.GetCollection<Item>("Phone");
          //  db.DropCollection("Phone");

            Console.WriteLine("Adding into database started...");
            
            foreach (string str in href)
            {
                var uri = str;
                var cancellationToken = new CancellationTokenSource();
                var httpClient = new HttpClient();
                HttpResponseMessage request = await httpClient.GetAsync(uri);
                cancellationToken.Token.ThrowIfCancellationRequested();

                //Get the response stream
                var response = await request.Content.ReadAsStreamAsync();
                cancellationToken.Token.ThrowIfCancellationRequested();

                //Parse the stream
                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.ParseDocument(response);
                //Do something with LINQ

                string header = document.QuerySelector("h2.item-content__el-heading").InnerHtml;
                header = header.Substring(20);
                string price = document.QuerySelector("div.item__price-once").InnerHtml;
                price = price.Trim();
                price = price.Substring(0, price.Length-2);
                //var ImgNode = document.QuerySelector("img.item__slider-thumb-pic");
                //string ImgUrl = ImgNode.Attributes["src"].Value;
                Dictionary<string, string> Props = new Dictionary<string,string>();
                AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> nodes = document.QuerySelectorAll("dl.specifications-list__spec");
                foreach (var node in nodes)
                {
                    string propName = node.QuerySelector("span").InnerHtml;
                    string propAttr = node.QuerySelector("dd").InnerHtml.Trim();
                    propAttr = propAttr.Replace("&nbsp;", " ");
                    Props[propName] = propAttr;
                }
                Item itemData = new Item
                {
                    itemHeader = header,
                    itemPrice = price,
                    itemProps = Props
                };
                await collection.InsertOneAsync(itemData);
                Console.WriteLine(header);
            }
            Console.WriteLine("Adding completed");
          //  Console.ReadKey(true);
        } 
    }
}
