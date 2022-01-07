namespace QuizSystemWeb.Services.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserAnswersServiceModel
    {
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }

        public string TextAnswer { get; set; }

        public int TestId { get; set; }
    }
}
