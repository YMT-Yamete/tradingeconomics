namespace Trading_Economics_News.Models
{
    public class News
    {
        public string id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string country { get; set; }
        public string category { get; set; }
        public string symbol { get; set; }
        public string url { get; set; }
        public  int importance { get; set; }
    }
}
