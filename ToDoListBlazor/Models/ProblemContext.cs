using Microsoft.EntityFrameworkCore;

namespace ToDoListBlazor.Models
{
    public partial class ProblemContext : DbContext
    {
        public ProblemContext()
        {
        }
        public ProblemContext(DbContextOptions<ProblemContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Problem> Problems { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Problem>(entity =>
            {
                entity.ToTable("ProblemSet");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Executors)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.StartDate);
                entity.Property(e => e.Status);
                entity.Property(e => e.PlannedComplexityTime);
                entity.Property(e => e.FactTime);
                entity.Property(e => e.FinishDate);
                entity.Property(e => e.SubProblems);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
