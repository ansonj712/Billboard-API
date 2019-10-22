using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BillboardsAPI.Models;
using HtmlAgilityPack;
using System.Text;
using Flurl;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;

namespace BillboardAPI.Controllers
{
    [ApiController]
    [Route("api/charts")]
    public class ChartsController : ControllerBase 
    {
        // Free Charts
        const string elementsXPathFree = "//li[contains(@class, 'chart-list__element')]";
        const string rankXPathFree = ".//span[@class='chart-element__rank__number']";
        const string songXPathFree = ".//span[contains(@class, 'chart-element__information__song')]";
        const string artistXPathFree = ".//span[contains(@class, 'chart-element__information__artist')]";
        const string lastWeekXPathFree = ".//span[contains(@class, 'color--secondary text--last')]";
        const string peakXPathFree = ".//span[contains(@class, 'color--secondary text--peak')]";
        const string durationXPathFree = ".//span[contains(@class, 'color--secondary text--week')]";

        // Paid Charts
        const string elementsSelectorPaid = ".chart-list-item";
        const string rankSelectorPaid = ".chart-list-item__rank";
        const string songSelectorPaid = ".chart-list-item__title-text";
        const string artistSelectorPaid = ".chart-list-item__artist";
        const string lastWeekSelectorPaid = ".chart-list-item__last-week";
        const string peakSelectorPaid = ".chart-list-item__weeks-at-one";
        const string durationSelectorPaid = ".chart-list-item__weeks-on-chart";

        [HttpGet]
        [Route("{name}/{date?}")]
        public IEnumerable<ChartListItem> GetChart(string name, string date)
        {
            List<ChartListItem> chart = new List<ChartListItem>();

            HtmlDocument doc = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();

            web.OverrideEncoding = Encoding.UTF8;

            string url = Flurl.Url.Combine("https://www.billboard.com/charts", name, date);

            doc = web.Load(url);

            IEnumerable<HtmlNode> elements;

            elements = doc.DocumentNode.SelectNodes(elementsXPathFree);

            if(elements == null) 
            {
                elements = doc.DocumentNode.QuerySelectorAll(elementsSelectorPaid);
            }

            foreach(var element in elements)
            {
                System.Console.WriteLine(element.InnerText);

                ChartListItem item = new ChartListItem();

                string rank;
                string song;
                string artist;
                string lastWeek;
                string peak;
                string duration;

                try 
                {
                    rank = element.SelectSingleNode(rankXPathFree).InnerText;
                    song = element.SelectSingleNode(songXPathFree).InnerText;
                    artist = element.SelectSingleNode(artistXPathFree).InnerText;
                    lastWeek = element.SelectSingleNode(lastWeekXPathFree).InnerText;
                    peak = element.SelectSingleNode(peakXPathFree).InnerText;
                    duration = element.SelectSingleNode(durationXPathFree).InnerText;
                }
                catch 
                {
                    rank = element.QuerySelector(rankSelectorPaid).InnerText.Trim();
                    song = element.QuerySelector(songSelectorPaid).InnerText.Trim();
                    artist = element.QuerySelector(artistSelectorPaid).InnerText.Trim();
                    lastWeek = element.QuerySelector(lastWeekSelectorPaid).InnerText.Trim();
                    peak = element.QuerySelector(peakSelectorPaid).InnerText.Trim();
                    duration = element.QuerySelector(durationSelectorPaid).InnerText.Trim();
                }

                item.Rank = Convert.ToInt32(rank);
                item.Song = HtmlEntity.DeEntitize(song);
                item.Artist = HtmlEntity.DeEntitize(artist);
                item.LastWeek = lastWeek != "-" ? Convert.ToInt32(lastWeek) : 0;
                item.Peak = Convert.ToInt32(peak);
                item.Duration = Convert.ToInt32(duration);

                chart.Add(item);
            }

            return chart;
        }
    }
}