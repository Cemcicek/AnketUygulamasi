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
        public AnswerController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
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

        [HttpGet("questionbyanswerlist")]
        public IActionResult QuestionByAnswerList()
        {
            var questions = _questionService.TGetListAll();
            var answers = _answerService.TGetListAll();
            var questionViewModels = from q in questions
                                     join s in answers on q.QuestionID equals s.QuestionID
                                     select new AnswerDto
                                     {
                                         QuestionID = q.QuestionID,
                                         QuestionName = q.QuestionName,
                                         Answerrs = s.Answerrs,
                                         Firstname = s.Firstname,
                                         Lastname = s.Lastname,
                                         Email = s.Email
                                     };

            return Ok(questionViewModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnswer(int id)
        {
            var value = _answerService.TGetByID(id);
            return Ok(value);
        }
    }
}
