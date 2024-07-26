using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace AnketUygulamasiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet]
        public IActionResult SurveyList() 
        {
            var values = _surveyService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateSurvey(Survey survey)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            survey.Status = true;
            _surveyService.TAdd(survey);
            return Ok();
        }

		[HttpDelete("{id}")]
		public IActionResult DeleteSurvey(int id)
        {
            var value = _surveyService.TGetByID(id);
            _surveyService.TDelete(value);
            return Ok();
        }

		[HttpGet("{id}")]
		public IActionResult GetSurvey(int id)
		{
			var value = _surveyService.TGetByID(id);
			return Ok(value);
		}

		[HttpPut]
        public IActionResult UpdateSurvey(UpdateSurveyDto updateSurveyDto)
        {
            Survey survey = new Survey()
            {
                SurveyID = updateSurveyDto.SurveyID,
                SurveyName = updateSurveyDto.SurveyName,
                Status = updateSurveyDto.Status
            };
            _surveyService.TUpdate(survey);
            return Ok();
		}
    }
}
