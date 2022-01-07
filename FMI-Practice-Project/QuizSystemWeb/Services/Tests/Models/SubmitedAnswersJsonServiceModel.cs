using System.Collections.Generic;

namespace QuizSystemWeb.Services.Tests.Models
{
    public class SubmitedAnswersJsonServiceModel
    {
        public int TestId { get; set; }

        public ICollection<UserAnswersServiceModel> Answers { get; set; }
    }
}
