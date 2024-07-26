using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnketUygulamasiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        private readonly ISurveyService _surveyService;
        public AnswerController(IAnswerService answerService, IQuestionService questionService, ISurveyService surveyService)
        {
            _answerService = answerService;
            _questionService = questionService;
            _surveyService = surveyService;
        }

        [HttpGet]
        public IActionResult AnswerList()
        {
            var values = _answerService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateAnswer(Answer answer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _answerService.TAdd(answer);
            return Ok();
        }

		[HttpGet("surveys")]
		public IActionResult GetSurveys()
		{
			var surveys = _surveyService.TGetListAll();
			var surveyDtos = surveys.Select(survey => new UpdateSurveyDto
			{
				SurveyID = survey.SurveyID,
				SurveyName = survey.SurveyName
			}).ToList();

			return Ok(surveyDtos);
		}


		[HttpGet("questionbyanswerlist/{surveyId}")]
		public IActionResult QuestionByAnswerList(int surveyId)
		{
			var surveys = _surveyService.TGetListAll();
			var questions = _questionService.TGetListAll();
			var answers = _answerService.TGetListAll();

			var surveyQuestionsAnswers = from survey in surveys
										 join question in questions on survey.SurveyID equals question.SurveyID
										 join answer in answers on question.QuestionID equals answer.QuestionID
										 where survey.SurveyID == surveyId
										 select new AnswerDto
										 {
											 SurveyID = survey.SurveyID,
											 SurveyName = survey.SurveyName,
											 QuestionID = question.QuestionID,
											 QuestionName = question.QuestionName,
											 Answerrs = answer.Answerrs,
											 Firstname = answer.Firstname,
											 Lastname = answer.Lastname,
											 Email = answer.Email
										 };

			return Ok(surveyQuestionsAnswers);
		}


		[HttpGet("{id}")]
        public IActionResult GetAnswer(int id)
        {
            var value = _answerService.TGetByID(id);
            return Ok(value);
        }
    }
}
