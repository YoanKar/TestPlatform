namespace QuizSystemWeb.Services.Answers
{
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Answers.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        bool Create(int questionId, string content, bool IsCorrect);

        bool Edit(int answerId, string content, bool IsCorrect);

        bool Delete(int questionId);

        AnswerDetailsServiceModel GetById(int id);

        ICollection<AnswerSignificanceServiceModel> GetAnswerSignificances();
    }
}
