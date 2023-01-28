using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BillboardAPI.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace BillboardAPI.Repository 
{
    public class ChartRepository
    {
        // Selectors
        const string ChartListItemSelector = ".o-chart-results-list-row-container";
        const string RankSelector = ".c-label";
        const string SongSelector = ".o-chart-results-list__item > #title-of-a-story";
        const string ArtistSelector = ".lrv-a-unstyle-list > li:nth-child(4) li .c-label";
        const string LastWeekSelector = ".lrv-a-unstyle-list > li:nth-child(4) > ul > li:nth-child(4)";
        const string PeakSelector = ".lrv-a-unstyle-list > li:nth-child(4) > ul > li:nth-child(5)";
        const string DurationSelector = ".lrv-a-unstyle-list > li:nth-child(4) > ul > li:nth-child(6)";

        public static List<ChartListItem> GetChart(string name)
        {
            string url = Flurl.Url.Combine("https://www.billboard.com/charts", name);

            return ParseWebpage(url);
        }

        public static List<ChartListItem> GetChart(string name, string date)
        {
            string url = Flurl.Url.Combine("https://www.billboard.com/charts", name, date);

            return ParseWebpage(url);
        }

        private static List<ChartListItem> ParseWebpage(string url)
        {
            List<ChartListItem> chart = new List<ChartListItem>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            web.OverrideEncoding = Encoding.UTF8;

            List<HtmlNode> chartListItemNodes = doc.DocumentNode.QuerySelectorAll(ChartListItemSelector).ToList();

            foreach (var chartListItemNode in chartListItemNodes)
            {
                ChartListItem chartListItem = new ChartListItem();

                string rank = "";
                string song = "";
                string artist = "";
                string lastWeek = "";
                string peak = "";
                string duration = "";

                try
                {
                    rank = chartListItemNode.QuerySelector(RankSelector).InnerText.Trim();
                    song = chartListItemNode.QuerySelector(SongSelector).InnerText.Trim();
                    artist = chartListItemNode.QuerySelector(ArtistSelector).InnerText.Trim();
                    lastWeek = chartListItemNode.QuerySelector(LastWeekSelector).InnerText.Trim();
                    peak = chartListItemNode.QuerySelector(PeakSelector).InnerText.Trim();
                    duration = chartListItemNode.QuerySelector(DurationSelector).InnerText.Trim();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                chartListItem.Rank = Convert.ToInt32(rank);
                chartListItem.Song = HtmlEntity.DeEntitize(song);
                chartListItem.Artist = HtmlEntity.DeEntitize(artist);
                chartListItem.LastWeek = lastWeek != "-" ? Convert.ToInt32(lastWeek) : 0;
                chartListItem.Peak = Convert.ToInt32(peak);
                chartListItem.Duration = Convert.ToInt32(duration);

                chart.Add(chartListItem);
            }

            return chart;
        }
    }
}