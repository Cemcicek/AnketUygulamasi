using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AnswerManager : IAnswerService
    {
        private readonly IAnswerDal _answerDal;
        public AnswerManager(IAnswerDal answerDal)
        {
            _answerDal = answerDal;
        }
        public void TAdd(Answer entity)
        {
            _answerDal.Add(entity);
        }

        public void TDelete(Answer entity)
        {
            _answerDal.Delete(entity);
        }

        public Answer TGetByID(int id)
        {
            return _answerDal.GetByID(id);
        }

        public List<Answer> TGetListAll()
        {
            return _answerDal.GetListAll();
        }

        public void TUpdate(Answer entity)
        {
            _answerDal.Update(entity);
        }
    }
}
