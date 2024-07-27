namespace AnketUygulamasiUI.Models.Dtos
{
    public class UpdateQuestionDto
    {
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public bool Status { get; set; }
        public int SurveyID { get; set; }
    }
}
