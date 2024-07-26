namespace AnketUygulamasiUI.Models.Dtos
{
	public class CreateQuestionDto
	{
        public string QuestionName { get; set; }
		public bool Status { get; set; }
		public int SurveyID { get; set; }
    }
}
