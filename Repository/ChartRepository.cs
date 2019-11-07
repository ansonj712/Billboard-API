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
        const string ChartListItemSelector = ".chart-list__element";
        const string RankSelector = ".chart-element__rank__number";
        const string SongSelector = ".chart-element__information__song";
        const string ArtistSelector = ".chart-element__information__artist";
        const string LastWeekSelector = ".chart-element__metas .text--last";
        const string PeakSelector = ".chart-element__metas .text--peak";
        const string DurationSelector = ".chart-element__metas .text--week";

        const string ChartListItemSelectorAlt = ".chart-list-item";
        const string RankSelectorAlt = ".chart-list-item__rank";
        const string SongSelectorAlt = ".chart-list-item__title-text";
        const string ArtistSelectorAlt = ".chart-list-item__artist";
        const string LastWeekSelectorAlt = ".chart-list-item__last-week";
        const string PeakSelectorAlt = ".chart-list-item__weeks-at-one";
        const string DurationSelectorAlt = ".chart-list-item__weeks-on-chart";

        public static List<ChartListItem> GetChart(string name, string date)
        {
            List<ChartListItem> chart = new List<ChartListItem>();

            HtmlDocument doc = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();
            List<HtmlNode> chartListItemNodes = new List<HtmlNode>();

            string url = Flurl.Url.Combine("https://www.billboard.com/charts", name, date);

            web.OverrideEncoding = Encoding.UTF8;

            doc = web.Load(url);

            chartListItemNodes = doc.DocumentNode.QuerySelectorAll(ChartListItemSelector).ToList();

            if(chartListItemNodes.Count == 0) 
            {
                chartListItemNodes = doc.DocumentNode.QuerySelectorAll(ChartListItemSelectorAlt).ToList();
            }

            foreach(var chartListItemNode in chartListItemNodes)
            {
                ChartListItem chartListItem = new ChartListItem();

                string rank = string.Empty;
                string song = string.Empty;
                string artist = string.Empty;
                string lastWeek = string.Empty;
                string peak = string.Empty;
                string duration = string.Empty;

                try 
                {
                    rank = chartListItemNode.QuerySelector(RankSelector).InnerText.Trim();
                    song = chartListItemNode.QuerySelector(SongSelector).InnerText.Trim();
                    artist = chartListItemNode.QuerySelector(ArtistSelector).InnerText.Trim();
                    lastWeek = chartListItemNode.QuerySelector(LastWeekSelector).InnerText.Trim();
                    peak = chartListItemNode.QuerySelector(PeakSelector).InnerText.Trim();
                    duration = chartListItemNode.QuerySelector(DurationSelector).InnerText.Trim();
                }
                catch
                {
                    rank = chartListItemNode.QuerySelector(RankSelectorAlt).InnerText.Trim();
                    song = chartListItemNode.QuerySelector(SongSelectorAlt).InnerText.Trim();
                    artist = chartListItemNode.QuerySelector(ArtistSelectorAlt).InnerText.Trim();
                    lastWeek = chartListItemNode.QuerySelector(LastWeekSelectorAlt).InnerText.Trim();
                    peak = chartListItemNode.QuerySelector(PeakSelectorAlt).InnerText.Trim();
                    duration = chartListItemNode.QuerySelector(DurationSelectorAlt).InnerText.Trim();
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