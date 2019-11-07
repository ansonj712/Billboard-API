namespace BillboardAPI.Models
{
    public class ChartListItem
    {
        public int Rank { get; set; }
        public string Song { get; set; }
        public string Artist { get; set; }
        public int? LastWeek { get; set; }
        public int Peak { get; set; }
        public int Duration { get; set; }
    }
}