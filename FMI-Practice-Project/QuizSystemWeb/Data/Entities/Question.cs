namespace QuizSystemWeb.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Question;

    public class Question
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public int Points { get; set; }

        public int QuestionTypeId { get; set; }

        public virtual QuestionType QuestionType { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        = new HashSet<Answer>();

     
    }
}
