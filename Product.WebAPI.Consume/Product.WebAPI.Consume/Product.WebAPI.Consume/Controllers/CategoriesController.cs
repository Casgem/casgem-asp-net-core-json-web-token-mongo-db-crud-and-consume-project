using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.WebAPI.Consume.DTOs.CategoryDTOs;
using Product.WebAPI.Consume.DTOs.ConvertingDTO;
using Product.WebAPI.Consume.Models;
using System.Text;

namespace Product.WebAPI.Consume.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        //[Route("/Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var responseMessage = await client.GetAsync("https://localhost:7216/api/Categories/");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var convertedValues = JsonConvert.DeserializeObject<ConvertingDTO>(jsonData);
                List<ResultCategoryDTO> results = new List<ResultCategoryDTO>();

                foreach (var data in convertedValues.data)
                {
                    results.Add(new ResultCategoryDTO
                    {
                        CategoryId = data.categoryId,
                        CategoryName = data.categoryName
                    });
                }
                //var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);
                return View(results);
            }
            return View();
        }

        [HttpGet]
        [Route("/AddCategory")]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("/AddCategory")]
        public async Task<IActionResult> AddCategory(CreateCategoryDTO createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7216/api/Categories/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Route("/DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Categories/DeleteCategory/{categoryId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(string categoryId, string categoryName)
        {
            UpdateCategoryDTO updateCategoryDTO = new UpdateCategoryDTO(categoryId, categoryName);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            //var responseMessage = await client.GetAsync($"https://localhost:7216/api/Categories/UpdateCategory/{categoryId}/{categoryName}");
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Categories/GetCategoryById/{categoryId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //var value = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData);
                var value = JsonConvert.DeserializeObject<ConvertingUpdateCategoryDTO>(jsonData);
                UpdateCategoryDTO updateCategory = new UpdateCategoryDTO()
                {
                    CategoryId = value.data.categoryId,
                    CategoryName = value.data.categoryName
                };
                return View(updateCategory);
            }
            return View();
        }

        /*
        [HttpPost]
        [Route("/UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7216/api/Categories/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }*/
        [HttpGet]
        [Route("/UpdateCategoryById")]
        public async Task<IActionResult> UpdateCategoryById(string categoryId, string categoryName)
        {
            UpdateCategoryDTO updateCategoryDTO = new UpdateCategoryDTO(categoryId, categoryName);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            //var responseMessage = await client.GetAsync($"https://localhost:7216/api/Categories/UpdateCategory/{categoryId}/{categoryName}");
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Categories/UpdateCategory/{categoryId}/{categoryName}");
            if (responseMessage.IsSuccessStatusCode)
            {
                /*var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData);*/

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}