using ToDoListBlazor.Models;

namespace ToDoListBlazor.Interfaces
{
    public interface IProblem
    {
        public List<Problem> GetProblemDetails();
        public int AddProblem(Problem user);
        public void UpdateProblemDetails(Problem user);
        public Problem GetProblemData(int id);
        public void DeleteProblem(int id);
    }
}
