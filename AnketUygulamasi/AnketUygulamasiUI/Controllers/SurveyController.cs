using AnketUygulamasiUI.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AnketUygulamasiUI.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SurveyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5155/api/Survey");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSurveyDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateSurvey()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey(CreateSurveyDto createSurveyDto)
        {
            createSurveyDto.Status = true;
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createSurveyDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("http://localhost:5155/api/Survey", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
                Alert("Başarılı!", "Başarıyla Eklenmiştir!", "success");
                return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<IActionResult> DeleteSurvey(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"http://localhost:5155/api/Survey/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				Alert("Başarılı!", "Başarıyla Silinmiştir!", "success");
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpGet]
        public async Task<IActionResult> UpdateSurvey(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5155/api/Survey/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateSurveyDto>(jsonData);
                return View(values);
            }
            return View();
		}

        [HttpPost]
		public async Task<IActionResult> UpdateSurvey(UpdateSurveyDto updateSurveyDto)
        {
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateSurveyDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5155/api/Survey/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}

		public void Alert(string title, string message, string icon)
		{
			TempData["Title"] = title;
			TempData["Message"] = message;
			TempData["Icon"] = icon;
		}

	}
}
