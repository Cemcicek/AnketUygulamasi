using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string Answerrs { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int QuestionID { get; set; }

    }

    public class AnswerDto
    {
        public int AnswerID { get; set; }
        public string Answerrs { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public int SurveyID { get; set; }
        public string SurveyName { get; set; }

    }
}
