using AnketUygulamasiUI.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
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

		public async Task<IActionResult> SurveysList()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("http://localhost:5155/api/Answer/surveys");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<UpdateSurveyDto>>(jsonData);
				return View(values);
			}
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> QuestionByAnswerList(int surveyId)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"http://localhost:5155/api/Answer/questionbyanswerlist/{surveyId}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultAnswerDto>>(jsonData);
				return View(values);
			}
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> ExportToExcel(int surveyId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5155/api/Answer/questionbyanswerlist/{surveyId}");
            if (!responseMessage.IsSuccessStatusCode)
                return RedirectToAction("QuestionByAnswerList");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAnswerDto>>(jsonData);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Answers");
                worksheet.Cells["A1"].Value = "SurveyName";
                worksheet.Cells["B1"].Value = "Question";
                worksheet.Cells["C1"].Value = "Answer";
                worksheet.Cells["D1"].Value = "First Name";
                worksheet.Cells["E1"].Value = "Last Name";
                worksheet.Cells["F1"].Value = "Email";

                int row = 2;
                foreach (var answer in values)
                {
                    worksheet.Cells[row, 1].Value = answer.SurveyName;
                    worksheet.Cells[row, 2].Value = answer.QuestionName;
                    worksheet.Cells[row, 3].Value = answer.Answerrs;
                    worksheet.Cells[row, 4].Value = answer.Firstname;
                    worksheet.Cells[row, 5].Value = answer.Lastname;
                    worksheet.Cells[row, 6].Value = answer.Email;
                    row++;
                }

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Answers.xlsx");
            }
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
