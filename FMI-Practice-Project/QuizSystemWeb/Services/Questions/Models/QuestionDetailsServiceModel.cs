namespace QuizSystemWeb.Services.Questions.Models
{
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Answers.Models;
    using System.Collections.Generic;
    public class QuestionDetailsServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Points { get; set; }

        public int QuestionTypeId { get; set; }

        public string QuestionType { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<AnswerDetailsServiceModel> Answers { get; set; }
    }
}
