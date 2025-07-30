using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Walks.MVC.Models.DTO;

namespace Walks.MVC.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7118/api/Regions/GetAll");

                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch(Exception ex)
            {

            }
     

            return View(response);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AddRegionRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddRegionRequest addRegionRequest)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7118/api/Regions/Create"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionRequest), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<AddRegionRequest>();
            if(response is not null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7118/api/Regions/{id}"); 

            if (response is not null)
            {
                return View(response);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO regionDTO)
        {
            var client=httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"https://localhost:7118/api/Regions/{regionDTO.Id}"),
                Method = HttpMethod.Put,
                Content = new StringContent(JsonSerializer.Serialize(regionDTO), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO regionDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var response = await client.DeleteAsync($"https://localhost:7118/api/Regions/{regionDTO.Id}");

                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

            }
            return View(nameof(Edit));
        }
    }
}
