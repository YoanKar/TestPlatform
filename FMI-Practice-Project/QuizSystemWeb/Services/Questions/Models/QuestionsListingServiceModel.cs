using System;
using System.Collections.Generic;

namespace QuizSystemWeb.Services.Questions.Models
{
    public class QuestionsListingServiceModel
    {
        public DateTime DeadLine { get; set; }
        public ICollection<QuestionDetailsServiceModel> QuestionsList { get; set; }
    }
}
