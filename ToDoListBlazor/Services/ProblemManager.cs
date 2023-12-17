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
        public int AddProblem(Problem problem)
        {
            try
            {
                _dbContext.Problems.Add(problem);
                _dbContext.SaveChanges();
                return problem.Id; // Возвращаем Id добавленного элемента
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
                Console.WriteLine("UPDATED: "+ problem.Id);
            }
            catch
            {
                throw;
            }
        }

        public void AddSubProblem(Problem mainProblem,Problem subProblem)
        {
            int subPId = AddProblem(subProblem);                        
            
            if (string.IsNullOrEmpty(mainProblem.SubProblemsId))
            {
                mainProblem.SubProblemsId = subPId.ToString();
            }
            else
            {
                mainProblem.SubProblemsId = $"{mainProblem.SubProblemsId}|{subPId}";
            }

            UpdateProblemDetails(mainProblem);

            Console.WriteLine($"SubProblem Added: {subPId}");
        }
    }
}
