using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult?> Index()
        {
            var view = View();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7137/api/contacts/get");
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            Console.WriteLine(response.StatusCode);
            view.ViewData.Add("Contacts", response.Content.ReadFromJsonAsync<Contact[]>().Result);
            return view;
        }

        [Route("/Home/OnPost", Name = "onpost")]
        public async Task<IActionResult?> OnPost()
        {
            Console.WriteLine("bitch called");
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"https://localhost:7137/api/contacts/post?Id={Request.Form["contactId"]}&Name={Request.Form["contactName"]}");
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            Console.WriteLine(response.StatusCode);
            return Index().Result;
        }
    }
}
