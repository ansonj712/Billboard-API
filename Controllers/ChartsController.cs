using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BillboardsAPI.Models;
using HtmlAgilityPack;
using System.Text;
using Flurl;

namespace BillboardAPI.Controllers
{
    [ApiController]
    [Route("api/charts")]
    public class ChartsController : ControllerBase 
    {
        [HttpGet]
        [Route("hot-100/{date?}")]
        public IEnumerable<ChartListItem> GetHot100Songs(string date)
        {
            List<ChartListItem> hot100Songs = new List<ChartListItem>();

            HtmlDocument doc = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();

            web.OverrideEncoding = Encoding.UTF8;

            string url = Flurl.Url.Combine("https://www.billboard.com/charts/hot-100", date);

            doc = web.Load(url);

            IEnumerable<HtmlNode> elements = doc.DocumentNode.SelectNodes("//li[contains(@class, 'chart-list__element')]");

            foreach(var element in elements)
            {
                ChartListItem item = new ChartListItem();

                string rank = element.SelectSingleNode(".//span[@class='chart-element__rank__number']").InnerText;
                string song = element.SelectSingleNode(".//span[contains(@class, 'chart-element__information__song')]").InnerText;
                string artist = element.SelectSingleNode(".//span[contains(@class, 'chart-element__information__artist')]").InnerText;
                string lastWeek = element.SelectSingleNode(".//span[contains(@class, 'color--secondary text--last')]").InnerText;
                string peak = element.SelectSingleNode(".//span[contains(@class, 'color--secondary text--peak')]").InnerText;
                string duration = element.SelectSingleNode(".//span[contains(@class, 'color--secondary text--week')]").InnerText;

                item.Rank = Convert.ToInt32(rank);
                item.Song = HtmlEntity.DeEntitize(song);
                item.Artist = HtmlEntity.DeEntitize(artist);
                item.LastWeek = lastWeek != "-" ? Convert.ToInt32(lastWeek) : 0;
                item.Peak = Convert.ToInt32(peak);
                item.Duration = Convert.ToInt32(duration);

                hot100Songs.Add(item);
            }

            return hot100Songs;
        }

        [HttpGet]
        [Route("billboard-200")]
        public IEnumerable<ChartListItem> GetBillboard200Songs(string date)
        {
            List<ChartListItem> billboard200Songs = new List<ChartListItem>();

            HtmlDocument doc = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();

            web.OverrideEncoding = Encoding.UTF8;

            string url = Flurl.Url.Combine("https://www.billboard.com/charts/billboard-200", date);

            doc = web.Load(url);

            IEnumerable<HtmlNode> elements = doc.DocumentNode.SelectNodes("//li[contains(@class, 'chart-list__element')]");

            foreach(var element in elements)
            {
                ChartListItem item = new ChartListItem();

                string rank = element.SelectSingleNode(".//span[@class='chart-element__rank__number']").InnerText;
                string song = element.SelectSingleNode(".//span[contains(@class, 'chart-element__information__song')]").InnerText;
                string artist = element.SelectSingleNode(".//span[contains(@class, 'chart-element__information__artist')]").InnerText;
                string lastWeek = element.SelectSingleNode(".//span[contains(@class, 'color--secondary text--last')]").InnerText;
                string peak = element.SelectSingleNode(".//span[contains(@class, 'color--secondary text--peak')]").InnerText;
                string duration = element.SelectSingleNode(".//span[contains(@class, 'color--secondary text--week')]").InnerText;

                item.Rank = Convert.ToInt32(rank);
                item.Song = HtmlEntity.DeEntitize(song);
                item.Artist = HtmlEntity.DeEntitize(artist);
                item.LastWeek = lastWeek != "-" ? Convert.ToInt32(lastWeek) : 0;
                item.Peak = Convert.ToInt32(peak);
                item.Duration = Convert.ToInt32(duration);

                billboard200Songs.Add(item);
            }

            return billboard200Songs;
        } 
    }
}