namespace AnketUygulamasiUI.Models.Dtos
{
    public class CreateAnswerDto
    {
        public int AnswerID { get; set; }
        public string Answerrs { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
    }
}
