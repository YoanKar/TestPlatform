namespace QuizSystemWeb.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Answer;

    public class Answer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public int IsCorrectId { get; set; }

        public AnswerSignificance IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

       


    }
}
