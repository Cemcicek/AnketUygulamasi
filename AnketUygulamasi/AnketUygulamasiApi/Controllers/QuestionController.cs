using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnketUygulamasiApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly IQuestionService _questionService;
		private readonly ISurveyService _surveyService;
        public QuestionController(IQuestionService questionService, ISurveyService surveyService)
        {
			_questionService = questionService;
			_surveyService = surveyService;
        }

		[HttpGet]
		public IActionResult QuestionList()
		{
			var questions = _questionService.TGetListAll();
			var surveys = _surveyService.TGetListAll();
			var questionViewModels = from q in questions
									 join s in surveys on q.SurveyID equals s.SurveyID
									 select new QuestionDto
									 {
										 QuestionID = q.QuestionID,
										 QuestionName = q.QuestionName,
										 Status = q.Status,
										 SurveyID = s.SurveyID,
										 SurveyName = s.SurveyName
									 };

			return Ok(questionViewModels);
		}

		[HttpGet("{id}")]
		public IActionResult GetQuestion(int id)
		{
			var value = _questionService.TGetByID(id);
			return Ok(value);
		}


		[HttpPost]
		public IActionResult CreateQuestion(Question question)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			question.Status = true;
			_questionService.TAdd(question);
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteQuestions(int id)
		{
			var value = _questionService.TGetByID(id);
			_questionService.TDelete(value);
			return Ok();
		}


        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionDto updateQuestionDto)
        {
            Question question = new Question()
            {
                QuestionID = updateQuestionDto.QuestionID,
				QuestionName = updateQuestionDto.QuestionName,
                Status = updateQuestionDto.Status
            };
            _questionService.TUpdate(question);
            return Ok();
        }
    }
}
