﻿using Microsoft.EntityFrameworkCore;
using ToDoListBlazor.Interfaces;
using ToDoListBlazor.Models;

namespace ToDoListBlazor.Services
{
    public class ProblemManager : IProblem
    {
        readonly ProblemContext _dbContext = new();
        public ProblemManager()
        {
        }
        public ProblemManager(ProblemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProblem(Problem problem)
        {
            try
            {
                _dbContext.Problems.Add(problem);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteProblem(int id)
        {
            try
            {
                Problem? problem = _dbContext.Problems.Find(id);
                if (problem != null)
                {
                    _dbContext.Problems.Remove(problem);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public Problem GetProblemData(int id)
        {
            try
            {
                Problem? problem = _dbContext.Problems.Find(id);
                if (problem != null)
                {
                    return problem;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<Problem> GetProblemDetails()
        {
            try
            {
                return _dbContext.Problems.ToList();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateProblemDetails(Problem problem)
        {
            try
            {
                _dbContext.Entry(problem).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
