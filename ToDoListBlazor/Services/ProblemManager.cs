using Microsoft.EntityFrameworkCore;
using ToDoListBlazor.Interfaces;
using ToDoListBlazor.Models;

namespace ToDoListBlazor.Services
{
    public class ProblemManager : IProblems
    {
        readonly ProblemContext _dbContext = new();
        public ProblemManager(ProblemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProblem(Problem user)
        {
            try
            {
                _dbContext.Problems.Add(user);
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
