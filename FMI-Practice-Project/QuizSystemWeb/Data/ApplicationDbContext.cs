namespace QuizSystemWeb.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using QuizSystemWeb.Data.Entities;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
      
         public DbSet<Answer> Answers { get; set; }
         public DbSet<AnswerSignificance> AnswerSignificances { get; set; }
         public DbSet<Question> Questions { get; set; }
         public DbSet<QuestionType> QuestionsTypes { get; set; }
         public DbSet<Test> Tests { get; set; }
         public DbSet<UsersAnswers> UsersAnswers { get; set; }

        public DbSet<Result> Results { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Question>()
                .HasOne(x => x.Test)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Answer>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

        }
    }
}
