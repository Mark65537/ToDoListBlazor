namespace ToDoListBlazor.Models
{
    public class ProblemContext : System.Data.Entity.DbContext
    {
        public ProblemContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Problem> Problems { get; set; }


    }

}
