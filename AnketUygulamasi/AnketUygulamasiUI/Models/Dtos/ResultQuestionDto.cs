namespace AnketUygulamasiUI.Models.Dtos
{
    public class ResultQuestionDto
    {
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public bool Status { get; set; }
        public int SurveyID { get; set; }
		public string SurveyName { get; set; }
	}
}
