using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using Trading_Economics_News.Models;
using static System.Net.WebRequestMethods;

namespace Trading_Economics_News.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string apiKey = "33de0237c94f42c:ye1arg9stl4v2qg";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            string url = "https://api.tradingeconomics.com/news?c=" + apiKey + "&f=json";
            var response = await _httpClient.GetAsync(url);
            IEnumerable<News> newsList = await response.Content.ReadAsAsync<IEnumerable<News>>();
            newsList = newsList.Where(n => n.id != "").ToList();
            return View(newsList);
        }

        public async Task<IActionResult> Search(string q)
        {
            string url = (q == null) ? "https://api.tradingeconomics.com/news?c=" + apiKey + "&f=json" :
                "https://api.tradingeconomics.com/news/country/" + q + "?c=" + apiKey + "&f=json";
            var response = await _httpClient.GetAsync(url);
            IEnumerable<News> newsList = await response.Content.ReadAsAsync<IEnumerable<News>>();
            newsList = newsList.Where(n => n.id != "").ToList();
            return PartialView("~/Views/Home/NewsPartial.cshtml", newsList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}