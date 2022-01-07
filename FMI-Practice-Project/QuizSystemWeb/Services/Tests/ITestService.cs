namespace QuizSystemWeb.Services.Tests
{
    using QuizSystemWeb.Services.Tests.Models;
    using System;
    using System.Collections.Generic;

    public interface ITestService
    {
        int SubmitUserAnswers(string input, string userId);

        bool Create(string name, DateTime startDate, DateTime endDate, TimeSpan duration, string authorId);

        bool Edit(int id,string name, DateTime startDate, DateTime endDate, TimeSpan duration);

        bool ChangeVisibility(int id);

        IEnumerable<TestServiceModel> CompletedTests(string userId);

        IEnumerable<TestServiceModel> GetAllTests();

        IEnumerable<ActiveTestsListingServiceModel> GetAllActiveTests(string userId);

        TestServiceDetailsModel Details(int id);

        ICollection<UnvaluatedTestsServiceModel> GetAllUnvaluatedTests();

        ICollection<EvaluatedTestsServiceModel> GetAllEvaluatedTests();

        ICollection<TestCheckAnswersServiceModel> CheckTest(int testId,string userId);

        ICollection<OpenQuestionAnswerServiceModel> GetOpenedAnswersForSolvedTest(string userId, int testId, int resultId);

        void AddPointsToResult(string json);

        void AddGradeToResult(string json);
    }
}
