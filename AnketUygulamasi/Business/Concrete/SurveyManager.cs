using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SurveyManager : ISurveyService
    {
        private readonly ISurveyDal _surveyDal;
        public SurveyManager(ISurveyDal surveyDal)
        {
            _surveyDal = surveyDal;
        }
        public void TAdd(Survey entity)
        {
            _surveyDal.Add(entity);
        }

        public void TDelete(Survey entity)
        {
            _surveyDal.Delete(entity);
        }

        public Survey TGetByID(int id)
        {
            return _surveyDal.GetByID(id);
        }

        public List<Survey> TGetListAll()
        {
            return _surveyDal.GetListAll();
        }

        public void TUpdate(Survey entity)
        {
            _surveyDal.Update(entity);
        }
    }
}
