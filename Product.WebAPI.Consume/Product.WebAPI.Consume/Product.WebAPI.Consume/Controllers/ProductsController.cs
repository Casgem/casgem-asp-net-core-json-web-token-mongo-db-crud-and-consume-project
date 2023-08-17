using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.WebAPI.Consume.DTOs.ProductDTOs;
using Product.WebAPI.Consume.DTOs.ConvertingDTO;
using System.Text;

namespace Product.WebAPI.Consume.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        //[Route("/Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var responseMessage = await client.GetAsync("https://localhost:7216/api/Products/");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var convertedValues = JsonConvert.DeserializeObject<ConvertingProductDTO>(jsonData);
                List<ResultProductDTO> results = new List<ResultProductDTO>();

                foreach (var data in convertedValues.data)
                {
                    results.Add(new ResultProductDTO
                    {
                        ProductId = data.productId,
                        ProductName = data.productName,
                        CategoryId = data.categoryId,
                        ProductDescripiton = data.productDescripiton,
                        ProductImage = data.productImage,
                        ProductPrice = data.productPrice,
                        ProductStock = data.productStock
                    });
                }
                //var values = JsonConvert.DeserializeObject<List<ResultProductDTO>>(jsonData);
                return View(results);
            }
            return View();
        }

        [HttpGet]
        [Route("/AddProduct")]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("/AddProduct")]
        public async Task<IActionResult> AddProduct(string categoryId, string productName)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            //var jsonData = JsonConvert.SerializeObject(createProductDto);
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Products/CreateProduct/{categoryId}/{productName}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Route("/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Products/DeleteProduct/{productId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(string productId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            //var responseMessage = await client.GetAsync($"https://localhost:7216/api/Products/UpdateProduct/{ProductId}/{ProductName}");
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Products/GetProductById/{productId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //var value = JsonConvert.DeserializeObject<UpdateProductDTO>(jsonData);
                var value = JsonConvert.DeserializeObject<ConvertingUpdateProductDTO>(jsonData);
                UpdateProductDTO updateProduct = new UpdateProductDTO
                {
                    ProductId = value.data.productId,
                    ProductName = value.data.productName,
                    CategoryId = value.data.categoryId,
                    ProductDescripiton = value.data.productDescripiton,
                    ProductImage = value.data.productImage,
                    ProductPrice = value.data.productPrice,
                    ProductStock = value.data.productStock
                };
                return View(updateProduct);
            }
            return View();
        }

        /*
        [HttpPost]
        [Route("/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7216/api/Products/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }*/
        [HttpGet]
        [Route("/UpdateProductById")]
        public async Task<IActionResult> UpdateProductById(string productId, string productName)
        {
            //UpdateProductDTO updateProductDTO = new UpdateProductDTO(productId, productName, 0, 0, productDescripiton, productImage, categoryId);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("my_token"));
            //var responseMessage = await client.GetAsync($"https://localhost:7216/api/Products/UpdateProduct/{ProductId}/{ProductName}");
            var responseMessage = await client.GetAsync($"https://localhost:7216/api/Products/UpdateProduct/{productId}/{productName}");
            if (responseMessage.IsSuccessStatusCode)
            {
                /*var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateProductDTO>(jsonData);*/

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
