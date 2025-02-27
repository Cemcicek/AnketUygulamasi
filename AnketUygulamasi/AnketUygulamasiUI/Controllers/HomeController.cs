using AnketUygulamasiUI.Models;
using AnketUygulamasiUI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AnketUygulamasiUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
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
                var filteredValues = values.Where(v => v.Status).ToList();
                return View(filteredValues);
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CreateAnswer(int id)
        {
        	var client = _httpClientFactory.CreateClient();


        	var surveyResponse = await client.GetAsync($"http://localhost:5155/api/Survey/{id}");
        	if (!surveyResponse.IsSuccessStatusCode)
        		return NotFound();

        	var surveyJsonData = await surveyResponse.Content.ReadAsStringAsync();
        	var survey = JsonConvert.DeserializeObject<ResultSurveyDto>(surveyJsonData);


        	var questionResponse = await client.GetAsync("http://localhost:5155/api/Question");
        	if (!questionResponse.IsSuccessStatusCode)
        		return NotFound();

        	var questionJsonData = await questionResponse.Content.ReadAsStringAsync();
        	var allQuestions = JsonConvert.DeserializeObject<List<ResultQuestionDto>>(questionJsonData);

            var surveyQuestions = allQuestions
                .Where(q => q.SurveyID == id)
                .Select(q => new QuestionDto
                {
                    QuestionID = q.QuestionID,
                    QuestionName = q.QuestionName
                })
                .ToList();

            var model = new SurveyDetailsDto
            {
                SurveyID = survey.SurveyID,
                SurveyName = survey.SurveyName,
                Questions = surveyQuestions
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer(SurveyDetailsDto model)
        {
            var client = _httpClientFactory.CreateClient();

            foreach (var question in model.Questions)
            {
                var answer = new AnswerDto
                {
                    QuestionID = question.QuestionID,
                    Answerrs = question.Answerrs,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email
                };

                var jsonData = JsonConvert.SerializeObject(answer);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("http://localhost:5155/api/Answer", stringContent);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
