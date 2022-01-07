namespace QuizSystemWeb.Services.Questions
{
    using QuizSystemWeb.Services.Questions.Models;
    using System.Collections.Generic;
   
    public interface IQuestionService
    {
        bool Create(string content, int points, int questionTypeId, int testId);

        bool Edit(int questionId, string content, int questionType,int points);

        ICollection<QuestionServiceModel> GetAllQuestions(int testId);

        ICollection<QuestionTypesServiceModel> GetQuestionTypes();

        QuestionDetailsServiceModel GetQuestionById(int questionId);

        public QuestionsListingServiceModel GetAllTestQuestions(int testId);
    }
}
