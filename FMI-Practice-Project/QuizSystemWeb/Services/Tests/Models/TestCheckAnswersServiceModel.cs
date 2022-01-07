namespace QuizSystemWeb.Services.Tests.Models
{
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Answers.Models;
    using QuizSystemWeb.Services.Questions.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestCheckAnswersServiceModel
    {

        public int QuestionId { get; set; }

        public string QuestionContent { get; set; }

        public int CorrectAnswerId { get; set; }

        public string CorrectAnswerContent { get; set; }

        public int MyAnswerId { get; set; }

        public string MyAnswerContent { get; set; }


    }
}
