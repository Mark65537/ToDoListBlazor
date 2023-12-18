using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using ToDoListBlazor.Interfaces;
using ToDoListBlazor.Models;

namespace ToDoListBlazor.Services
{
    public class ProblemManager : IProblem
    {
        private char separator = '|';

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
            try
            {
                Problem? problem = _dbContext.Problems.Find(id);
                if (problem != null)
                {
                    _dbContext.Problems.Remove(problem);
                    _dbContext.SaveChanges();
                    Console.WriteLine("Deleted: " + id);
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

        public async Task<List<Problem>> GetProblemDetailsAsync()
        {
            try
            {
                return await _dbContext.Problems.ToListAsync();
            }
            catch (SqlNullValueException ex)
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

        public int CalculatePlannedComplexityTime(Problem problem)
        {
            int totalComplexityTime = 0;

            if (!string.IsNullOrEmpty(problem.SubProblemsId))
            {
                IEnumerable<int> subProblemIds = null;

                if (problem.SubProblemsId.Contains(separator))
                {
                    subProblemIds = problem.SubProblemsId.Split(separator).Select(int.Parse);
                }
                else
                {
                    subProblemIds = Enumerable.Repeat(int.Parse(problem.SubProblemsId), 1);
                }

                var subProblems = subProblemIds.Select(id => GetProblemData(id))
                                   .Where(subProblem => subProblem != null)
                                   .ToList();
                foreach (var subProblem in subProblems)
                {
                    totalComplexityTime += CalculatePlannedComplexityTime(subProblem);
                }
                totalComplexityTime += problem.PlannedComplexityTime;
            }
            else
            {
                totalComplexityTime += problem.PlannedComplexityTime;
            }

            return totalComplexityTime;
        }

        public int CalculateFactTime(Problem problem)
        {
            int totalFactTime = 0;

            if (!string.IsNullOrEmpty(problem.SubProblemsId))
            {
                IEnumerable<int> subProblemIds = null;

                if (problem.SubProblemsId.Contains(separator))
                {
                    subProblemIds = problem.SubProblemsId.Split(separator).Select(int.Parse);
                }
                else
                {
                    subProblemIds = Enumerable.Repeat(int.Parse(problem.SubProblemsId), 1);
                }

                var subProblems = subProblemIds.Select(id => GetProblemData(id))
                                   .Where(subProblem => subProblem != null)
                                   .ToList();
                foreach (var subProblem in subProblems)
                {
                    if (subProblem.FactTime == null)
                    {
                        subProblem.FinishDate = DateTime.Now;
                        subProblem.FactTime = (int)subProblem.FinishDate.Value
                                                .Subtract(subProblem.StartDate).TotalDays;
                    }                    
                    totalFactTime += CalculateFactTime(subProblem);
                }
            }
            else
            {
                problem.FinishDate = DateTime.Now;
                problem.FactTime = (int)problem.FinishDate.Value
                                        .Subtract(problem.StartDate).TotalDays;
                totalFactTime += problem.FactTime.Value;
            }

            return totalFactTime;
        }

        public void DoneAllSubProblems(Problem problem)
        {
            if (!string.IsNullOrEmpty(problem.SubProblemsId))
            {
                IEnumerable<int> subProblemIds = null;

                if (problem.SubProblemsId.Contains(separator))
                {
                    subProblemIds = problem.SubProblemsId.Split(separator).Select(int.Parse);
                }
                else
                {
                    subProblemIds = Enumerable.Repeat(int.Parse(problem.SubProblemsId), 1);
                }

                var subProblems = subProblemIds.Select(id => GetProblemData(id))
                                   .Where(subProblem => subProblem != null)
                                   .ToList();
                foreach (var subProblem in subProblems)
                {
                    subProblem.FinishDate = DateTime.Now;
                    subProblem.FactTime = (int)subProblem.FinishDate.Value
                                                        .Subtract(subProblem.StartDate).TotalDays;
                    subProblem.Status = ProblemStatus.DONE;

                    DoneAllSubProblems(subProblem);
                }
            }
        }
        public bool CheckSubProblems(Problem problem)
        {
            if (!string.IsNullOrEmpty(problem.SubProblemsId))
            {
                IEnumerable<int> subProblemIds;

                if (problem.SubProblemsId.Contains(separator))
                {
                    subProblemIds = problem.SubProblemsId.Split(separator).Select(int.Parse);
                }
                else
                {
                    subProblemIds = Enumerable.Repeat(int.Parse(problem.SubProblemsId), 1);
                }

                var subProblems = subProblemIds.Select(GetProblemData)
                                              .Where(subProblem => subProblem != null)
                                              .ToList();

                foreach (var subProblem in subProblems)
                {
                    if (subProblem.Status != ProblemStatus.PAUSED)
                    {
                        if (problem.Status != ProblemStatus.DONE)
                        {
                            return false;
                        }
                    }

                    CheckSubProblems(subProblem);
                }

                return true;
            }

            return problem.Status == ProblemStatus.PAUSED || problem.Status == ProblemStatus.DONE;
        }



        private string CalculateAndSaveDuration(TimeSpan? duration)
        {
            if (duration == null)
            {
                throw new ArgumentNullException(nameof(duration));
            }

            // Преобразуем duration в нужный формат
            string formattedDuration = "";
            if (duration.Value.TotalDays >= 365)
            {
                int years = (int)(duration.Value.TotalDays / 365);
                formattedDuration += $"{years} год(а) ";
            }
            if (duration.Value.TotalDays >= 7)
            {
                int weeks = (int)(duration.Value.TotalDays / 7);
                formattedDuration += $"{weeks} неделя(и) ";
            }
            if (duration.Value.TotalDays >= 1)
            {
                int days = (int)duration.Value.TotalDays;
                formattedDuration += $"{days} день(дней)";
            }
            if (duration.Value.TotalHours >= 1)
            {
                int hours = (int)duration.Value.TotalHours;
                formattedDuration += $"{hours} час(ов)";
            }
            if (duration.Value.TotalMinutes >= 1)
            {
                int minutes = (int)duration.Value.TotalMinutes;
                formattedDuration += $"{minutes} минута(ы)";
            }
            if (duration.Value.TotalSeconds >= 1)
            {
                int seconds = (int)duration.Value.TotalSeconds;
                formattedDuration += $"{seconds} секунда(ы)";
            }

            // Сохраняем вычисленное значение в FactTime
            return formattedDuration;
        }


    }
}
