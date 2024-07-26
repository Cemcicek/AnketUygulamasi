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
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _questionDal;
       
        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }
        public void TAdd(Question entity)
        {
            _questionDal.Add(entity);
        }

        public void TDelete(Question entity)
        {
            _questionDal.Delete(entity);
        }

        public Question TGetByID(int id)
        {
            return _questionDal.GetByID(id);
        }

        public List<Question> TGetListAll()
        {
            return _questionDal.GetListAll();
        }

        public void TUpdate(Question entity)
        {
            _questionDal.Update(entity);
        }
    }
}
