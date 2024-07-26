using AnketUygulamasiUI.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AnketUygulamasiUI.Controllers
{
    [Authorize]
    public class QuestionController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
        public QuestionController(IHttpClientFactory httpClientFactory)
        {
			_httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5155/api/Question");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultQuestionDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> QuestionByAnswerList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5155/api/Answer/questionbyanswerlist");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAnswerDto>>(jsonData);
                return View(values);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CreateQuestion(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5155/api/Question/{id}");

            var values = new CreateQuestionDto { SurveyID = id };

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiValues = JsonConvert.DeserializeObject<CreateQuestionDto>(jsonData);

                if (apiValues != null)
                {
                    apiValues.SurveyID = id;
                    values = apiValues;
                }
            }

            return View(values);
        }

        [HttpPost]
		public async Task<IActionResult> CreateQuestion(CreateQuestionDto createQuestionDto)
		{
			createQuestionDto.Status = true;
			
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createQuestionDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("http://localhost:5155/api/Question", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View(createQuestionDto);
		}

        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5155/api/Question/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                Alert("Başarılı!", "Başarıyla Silinmiştir!", "success");
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
