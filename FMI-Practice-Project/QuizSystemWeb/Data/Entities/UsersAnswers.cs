namespace QuizSystemWeb.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Answer;

    public class UsersAnswers
    {
        public int Id { get; set; }

        [MaxLength(AnswerTextMaxLength)]
        public string AnswerText { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int? AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
