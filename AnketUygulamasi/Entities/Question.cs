using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public bool Status { get; set; }
        public int SurveyID { get; set; }
	}

	public class QuestionDto
	{
		public int QuestionID { get; set; }
		public string QuestionName { get; set; }
		public bool Status { get; set; }
		public int SurveyID { get; set; }
        public string SurveyName { get; set; }
    }
    public class UpdateQuestionDto
    {
        public int QuestionID { get; set; }
        public string QuestionName { get; set; }
        public bool Status { get; set; }
        public int SurveyID { get; set; }
        public string SurveyName { get; set; }
    }

}
