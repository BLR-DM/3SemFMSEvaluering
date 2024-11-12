﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMSEvaluering.Domain.Entities;

namespace FMSEvaluering.Application.Repositories
{
    public interface IEvaluationPostRepository
    {
        Task AddEvaluationPost(EvaluationPost evaluationPost);
    }
}
