using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
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
            Console.WriteLine("Deleted: " + id);
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

        public async Task DeleteProblemAsync(int id)
        {
            Console.WriteLine("Deleted: " + id);
            try
            {
                Problem? problem = await _dbContext.Problems.FindAsync(id);
                if (problem != null)
                {
                    _dbContext.Problems.Remove(problem);
                    await _dbContext.SaveChangesAsync();
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
            //List<Problem>? problems;
            try
            {
                return _dbContext.Problems.ToList();                
            }
            catch(SqlNullValueException ex)
            {               
               Console.WriteLine(ex.ToString());                    
               return null;
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

        public void AddSubProblem(int id)
        {
            //Problem problem = _dbContext.Problems.Find(id) ?? throw new ArgumentNullException();
            //problem.SubProblemsId += "|" + id;
            //_dbContext.SaveChanges();
        }
    }
}
