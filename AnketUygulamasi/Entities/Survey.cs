using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Survey
    {
        public int SurveyID { get; set; }
        public string SurveyName { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateSurveyDto
    {
		public int SurveyID { get; set; }
		public string SurveyName { get; set; }
		public bool Status { get; set; }
	}
}
