using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.WebAPI.Consume.DTOs.CategoryDTOs;
using Product.WebAPI.Consume.Models;
using System.Net.Http;

namespace Product.WebAPI.Consume.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthenticateController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7216/api/Default/Login");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //var values = JsonConvert.DeserializeObject<TokenViewModel>(jsonData);
                
                return View(new TokenViewModel(jsonData));
            }
            return View(new TokenViewModel("Token yok sunucuya bağlanılamadı."));
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginViewModel.Token);
            var responseMessage = await client.GetAsync("https://localhost:7216/api/Default/Login");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("my_token", jsonData);
                return RedirectToAction("Index", "Categories");
            }
            return View(new TokenViewModel("Token yok sunucuya bağlanılamadı."));
        }
    }
}
