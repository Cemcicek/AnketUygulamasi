﻿using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfAnswerDal : GenericRepository<Answer>, IAnswerDal
    {
        public EfAnswerDal(ApplicationDBContext context) : base(context)
        {
            
        }
    }
}
