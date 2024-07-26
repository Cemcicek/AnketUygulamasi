namespace AnketUygulamasiUI.Models.Dtos
{
	public class SurveyDetailsDto
	{
        public int SurveyID { get; set; }
        public string SurveyName { get; set; }
        public List<QuestionDto> Questions { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }

	public class QuestionDto
	{
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public string Answerrs { get; set; } // Cevapları saklamak için
    }

	public class AnswerDto
	{
        public string Answerrs { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int QuestionID { get; set; }
    }
}
